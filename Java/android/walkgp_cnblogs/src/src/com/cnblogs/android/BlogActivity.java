package com.cnblogs.android;

import java.util.ArrayList;
import java.util.List;

import android.annotation.SuppressLint;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.res.Resources;
import android.graphics.Color;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.ContextMenu;
import android.view.ContextMenu.ContextMenuInfo;
import android.view.LayoutInflater;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnCreateContextMenuListener;
import android.widget.AdapterView;
import android.widget.AbsListView.OnScrollListener;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.AbsListView;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import com.cnblogs.android.adapter.BlogListAdapter;
import com.cnblogs.android.adapter.BlogListAdapter.BlogViewHolder;
import com.cnblogs.android.core.BlogHelper;
import com.cnblogs.android.core.Config;
import com.cnblogs.android.entity.Blog;
import com.cnblogs.android.entity.BlogCategory;
import com.cnblogs.android.utility.BlogListHtmlParse;
import com.cnblogs.android.utility.NetHelper;
import com.cnblogs.android.controls.PullToRefreshListView;
import com.cnblogs.android.controls.PullToRefreshListView.OnRefreshListener;
import com.cnblogs.android.dal.BlogDalHelper;

/**
 * 博客列表
 * 
 * @author walkingp
 * @date:2011-12
 * 
 */
public class BlogActivity extends BaseMainActivity {

	int currentPageIndex = 1;// 页码

	ListView listView;
	private BlogListAdapter adapter;// 数据源
	List<Blog> listBlog = new ArrayList<Blog>();
	ProgressBar blogBody_progressBar;// 主题ListView加载框

	ImageButton blog_show_category;// 显示分类
	ImageButton blog_refresh_btn;// 刷新按钮
	ProgressBar blog_progress_bar;// 加载按钮

	private LinearLayout viewFooter;// footer view

	Resources res;// 资源
	private int lastItem;
	BlogDalHelper dbHelper;
	BlogCategory category;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		this.setContentView(R.layout.blog_layout);

		res = this.getResources();
		InitialControls();
		BindControls();
		new PageTask(0).execute();

		// 注册广播
		UpdateListViewReceiver receiver = new UpdateListViewReceiver();
		IntentFilter filter = new IntentFilter();
		filter.addAction("android.cnblogs.com.update_bloglist");
		registerReceiver(receiver, filter);
	}

	// 长按菜单响应函数
	@Override
	public boolean onContextItemSelected(MenuItem item) {

		AdapterView.AdapterContextMenuInfo menuInfo = (AdapterView.AdapterContextMenuInfo) item
				.getMenuInfo();
		Blog blog = getBlogByViewHolderContainer(menuInfo.targetView);

		if (blog == null)
			return false;

		int itemIndex = item.getItemId();

		switch (itemIndex) {
		case R.id.menu_blog_view:// 详细
			RedirectDetailActivity(blog);
			break;
		case R.id.menu_blog_comment:// 评论
			RedirectCommentActivity(blog);
			break;
		case R.id.menu_blog_author:// 博主所有随笔
			RedirectAuthorActivity(blog);
			break;
		case R.id.menu_blog_browser:// 在浏览器中查看
			ViewInBrowser(blog);
			break;
		case R.id.menu_blog_share:// 分享到
			ShareTo(blog);
			break;
		}

		return super.onContextItemSelected(item);
	}

	/**
	 * 初始化列表
	 */
	private void InitialControls() {
		listView = (ListView) findViewById(R.id.blog_list);
		blogBody_progressBar = (ProgressBar) findViewById(R.id.blogList_progressBar);
		blogBody_progressBar.setVisibility(View.VISIBLE);
		// 顶部工具啦
		blog_show_category = (ImageButton) findViewById(R.id.blog_show_category);
		blog_refresh_btn = (ImageButton) findViewById(R.id.blog_refresh_btn);
		blog_progress_bar = (ProgressBar) findViewById(R.id.blog_progressBar);
		// 底部view
		LayoutInflater mInflater = (LayoutInflater) getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		viewFooter = (LinearLayout) mInflater.inflate(R.layout.listview_footer,
				null, false);
		dbHelper = new BlogDalHelper(getApplicationContext());
	}

	/**
	 * 绑定事件
	 */
	private void BindControls() {
		// 刷新
		blog_refresh_btn.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
				new PageTask(-2).execute();
			}
		});
		blog_show_category.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
				Intent it = new Intent(BlogActivity.this,
						BlogCategoryActivity.class);
				Bundle bundle = new Bundle();
				bundle.putString("category", "");
				it.putExtras(bundle);
				startActivityForResult(it, Config.REQUEST_BLOG_CAGEGORY);
			}
		});
		// 上拉刷新
		((PullToRefreshListView) listView)
				.setOnRefreshListener(new OnRefreshListener() {
					@Override
					public void onRefresh() {
						new PageTask(-1).execute();
					}
				});
		// 下拉加载更多
		listView.setOnScrollListener(new OnScrollListener() {
			/**
			 * 下拉到最后一行
			 */
			@Override
			public void onScrollStateChanged(AbsListView view, int scrollState) {
				if (lastItem >= adapter.getCount()
						&& scrollState == OnScrollListener.SCROLL_STATE_IDLE) {
					new PageTask(currentPageIndex + 1).execute();
				}
			}

			@Override
			public void onScroll(AbsListView view, int firstVisibleItem,
					int visibleItemCount, int totalItemCount) {
				lastItem = firstVisibleItem - 2 + visibleItemCount;
			}
		});
		// 点击跳转
		listView.setOnItemClickListener(new OnItemClickListener() {
			@Override
			public void onItemClick(AdapterView<?> parent, View v,
					int position, long id) {
				Blog blog = getBlogByViewHolderContainer(v);
				if (blog != null)
					RedirectDetailActivity(blog);
			}
		});
		// 长按事件
		listView.setOnCreateContextMenuListener(new OnCreateContextMenuListener() {
			@Override
			public void onCreateContextMenu(ContextMenu menu, View v,
					ContextMenuInfo menuInfo) {
				MenuInflater inflater = getMenuInflater();
				inflater.inflate(R.menu.blog_list_contextmenu, menu);
				menu.setHeaderTitle(R.string.menu_bar_title);
			}
		});
	}

	/**
	 * 跳转到评论
	 * 
	 * @param v
	 */
	private void RedirectCommentActivity(Blog blog) {

		if (blog.GetCommentNum() == 0) {
			Toast.makeText(getApplicationContext(), R.string.sys_empty_comment,
					Toast.LENGTH_SHORT).show();
			// return;
		}
		Intent intent = new Intent();
		intent.setClass(BlogActivity.this, CommentActivity.class);
		Bundle bundle = new Bundle();
		bundle.putInt("contentId", blog.GetBlogId());
		bundle.putInt("commentType", 0);
		bundle.putString("title", blog.GetBlogTitle());
		bundle.putString("url", blog.GetBlogUrl());
		intent.putExtras(bundle);

		startActivity(intent);
	}

	/**
	 * 跳转到详情
	 * 
	 * @param v
	 */
	private void RedirectDetailActivity(Blog blog) {
		try {
			Intent intent = new Intent();
			intent.setClass(BlogActivity.this, BlogDetailActivity.class);
			Bundle bundle = new Bundle();
			bundle.putSerializable("blog", blog);
			intent.putExtras(bundle);
			startActivity(intent);

		} catch (Exception ex) {
			ex.printStackTrace();
		}
	}

	/**
	 * 在浏览器中查看
	 * 
	 * @param v
	 */
	private void ViewInBrowser(Blog blog) {
		Uri blogUri = Uri.parse(blog.GetBlogUrl());
		Intent it = new Intent(Intent.ACTION_VIEW, blogUri);
		startActivity(it);
	}

	/**
	 * 跳转到博主所有随笔
	 * 
	 * @param v
	 */
	private void RedirectAuthorActivity(Blog blog) {

		if (blog.GetUserName().equals("")) {
			Toast.makeText(getApplicationContext(), R.string.sys_no_author,
					Toast.LENGTH_SHORT).show();
			return;
		}

		Intent intent = new Intent();
		intent.setClass(BlogActivity.this, AuthorBlogActivity.class);
		Bundle bundle = new Bundle();
		bundle.putString("author", blog.GetUserName());
		bundle.putString("blogName", blog.GetAuthor());

		intent.putExtras(bundle);

		startActivity(intent);
	}

	/**
	 * 分享到
	 * 
	 * @param v
	 */
	private void ShareTo(Blog blog) {

		String shareContent = "《" + blog.GetBlogTitle() + "》,作者："
				+ blog.GetAuthor() + "，原文链接：" + blog.GetBlogUrl() + " 分享自："
				+ res.getString(R.string.app_name) + "Android客户端("
				+ res.getString(R.string.app_homepage) + ")";

		Intent intent = new Intent(Intent.ACTION_SEND);
		intent.setType("text/plain");
		intent.putExtra(Intent.EXTRA_SUBJECT, "请选择分享到…");
		intent.putExtra(Intent.EXTRA_TEXT, shareContent);
		startActivity(Intent.createChooser(intent, blog.GetBlogTitle()));
	}

	public Blog getBlogByViewHolderContainer(View v) {
		Object tag = v.getTag();
		Blog blog = null;
		if (tag != null && tag instanceof BlogViewHolder) {
			BlogViewHolder blogView = (BlogViewHolder) tag;
			blog = blogView.blog;
			if (blog == null) {
				Toast.makeText(getApplicationContext(), "找不到该Blog",
						Toast.LENGTH_SHORT).show();
			}
		}
		return blog;
	}

	/**
	 * 更新ListView为已读状态 此广播同时从BlogDeatail和DownloadServices
	 * 
	 * @author walkingp
	 * 
	 */
	public class UpdateListViewReceiver extends BroadcastReceiver {

		@Override
		public void onReceive(Context content, Intent intent) {

			Bundle bundle = intent.getExtras();
			int[] blogIdArr = bundle.getIntArray("blogIdArray");
			for (int i = 0, len = listView.getChildCount(); i < len; i++) {
				View view = listView.getChildAt(i);
				TextView tvId = (TextView) view
						.findViewById(R.id.recommend_text_id);
				if (tvId != null) {

					int blogId = Integer.parseInt(tvId.getText().toString());

					ImageView icoDown = (ImageView) view
							.findViewById(R.id.icon_downloaded);
					TextView tvTitle = (TextView) view
							.findViewById(R.id.recommend_text_title);

					for (int j = 0, size = blogIdArr.length; j < size; j++) {
						if (blogId == blogIdArr[j]) {
							icoDown.setVisibility(View.VISIBLE);// 已经离线
							tvTitle.setTextColor(Color.BLUE);// 已读
						}
					}

				}
			}
			for (int i = 0, len = blogIdArr.length; i < len; i++) {
				for (int j = 0, size = listBlog.size(); j < size; j++) {
					if (blogIdArr[i] == listBlog.get(j).GetBlogId()) {
						listBlog.get(i).SetIsFullText(true);
						listBlog.get(i).SetIsReaded(true);
					}
				}
			}
		}
	}

	/**
	 * 多线程启动（用于上拉加载、初始化、下载加载、刷新）
	 * 
	 */
	public class PageTask extends AsyncTask<String, Integer, List<Blog>> {
		int curPageIndex = 0;
		boolean isDataFromLocal = false;// 是否是从本地读取的数据

		int requestPageIndex = 0;

		public PageTask(int page) {
			curPageIndex = page;

		}

		protected List<Blog> doInBackground(String... params) {
			boolean isNetworkAvailable = NetHelper
					.networkIsAvailable(getApplicationContext());

			requestPageIndex = curPageIndex;
			if (requestPageIndex <= 0) {
				requestPageIndex = 1;
			}
			if (curPageIndex == -2) {
				requestPageIndex = currentPageIndex;
			}

			// 优先读取本地数据
			List<Blog> listBlogLocal = dbHelper.GetBlogListByPage(
					category==null?"":category.CategoryId, requestPageIndex,
					Config.BLOG_PAGE_SIZE);
			List<Blog> listBlogNet = new ArrayList<Blog>();
			if (isNetworkAvailable) {
				if (category == null || category.equals("")) {
					listBlogNet
							.addAll(BlogHelper.GetBlogList(requestPageIndex));
				} else {
					listBlogNet.addAll(BlogListHtmlParse.getBlogListByCategory(
							category.CategoryType, category.ParentCategoryId,
							category.CategoryId, requestPageIndex));
				}

			}

			List<Blog> newBlogs = isNetworkAvailable ? listBlogNet
					: listBlogLocal;
			isDataFromLocal = isNetworkAvailable ? false : true;

			List<Blog> additionalBlogs = new ArrayList<Blog>();

			for (Blog blog : newBlogs) {
				if (!listBlog.contains(blog)) {
					additionalBlogs.add(blog);
				}
			}

			switch (curPageIndex) {
			case -2:// 刷新当前请求页数据 将新增数据添加列表尾端
				return additionalBlogs;
			case -1:// 下拉刷新 将新增数据插入列表
				return additionalBlogs;
			case 0:// 首次加载 列表Clear后，将返回数据填充列表
				if (isNetworkAvailable) {
					isDataFromLocal = false;
					return listBlogNet;
				} else {
					isDataFromLocal = true;
					return listBlogLocal;
				}

			default:// 加载更多数据 将新增数据添加列表尾端 当前页加1
				return additionalBlogs;
			}
		}

		@Override
		protected void onCancelled() {
			super.onCancelled();
		}

		/**
		 * 加载内容
		 */
		/*
		 * (non-Javadoc)
		 * 
		 * @see android.os.AsyncTask#onPostExecute(java.lang.Object)
		 */
		@Override
		protected void onPostExecute(List<Blog> result) {

			/*	Toast.makeText(getApplicationContext(),
						"加载第" + String.valueOf(requestPageIndex) + "页",
						Toast.LENGTH_SHORT).show();*/
			// 右上角
			blogBody_progressBar.setVisibility(View.GONE);
			blog_progress_bar.setVisibility(View.GONE);
			blog_refresh_btn.setVisibility(View.VISIBLE);

			if (result == null || result.size() == 0) {// 没有新数据
				((PullToRefreshListView) listView).onRefreshComplete();
				boolean isNetworkAvailable = NetHelper
						.networkIsAvailable(getApplicationContext());
				String tips = "";
				if (curPageIndex > 1) {
					tips = "已经没有更多数据了。";
				} else if (curPageIndex == -1) {
					tips = "最近无更新。";
				} else if (curPageIndex == 0) {
					tips = isNetworkAvailable ? "本地无缓存。" : "请检查网络";
				}
				if (tips.length() != 0)
					Toast.makeText(getApplicationContext(), tips,
							Toast.LENGTH_SHORT).show();
				return;
			}
			int size = result.size();
			if (size >= Config.BLOG_PAGE_SIZE
					&& listView.getFooterViewsCount() == 0) {
				listView.addFooterView(viewFooter);
			}
			// 保存到数据库
			if (!isDataFromLocal) {
				//dbHelper.SynchronyData2DB(result);
			}
			if (adapter == null) {
				adapter = new BlogListAdapter(getApplicationContext(),
						listBlog, listView);
				listView.setAdapter(adapter);
				((PullToRefreshListView) listView)
						.SetPageSize(Config.BLOG_PAGE_SIZE);
			}

			boolean refresh = true;
			switch (curPageIndex) {
			case -2:// 刷新当前页
				adapter.AddMoreData(result);
				refresh = false;
				break;
			case -1:// 加载最新的
				adapter.InsertData(result);
				break;
			case 0:// 首次加载
				adapter.GetData().clear();
				adapter.AddMoreData(result);
				break;
			default:// 加载下一页
				adapter.AddMoreData(result);
				currentPageIndex = currentPageIndex + 1;
				refresh = false;
				break;
			}
			((PullToRefreshListView) listView).SetDataRow(adapter.GetData()
					.size());
			if (refresh)
				((PullToRefreshListView) listView).onRefreshComplete();
		}

		@Override
		protected void onPreExecute() {
			// 主体进度条
			if (listView.getCount() == 0) {
				blogBody_progressBar.setVisibility(View.VISIBLE);
			}
			// 右上角
			blog_progress_bar.setVisibility(View.VISIBLE);
			blog_refresh_btn.setVisibility(View.GONE);

		}

		@Override
		protected void onProgressUpdate(Integer... values) {
		}
	}

	/**
	 * 复写onActivityResult，这个方法 是要等到SimpleTaskActivity点了提交过后才会执行的
	 */
	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		// 可以根据多个请求代码来作相应的操作
		if (Config.REQUEST_BLOG_CAGEGORY == resultCode) {

			category = (BlogCategory) data
					.getSerializableExtra(Config.SELECTED_BLOG_CAGEGORY);
			new PageTask(0).execute();
			// Toast.makeText(getBaseContext(), category, 1000).show();
		}
		super.onActivityResult(requestCode, resultCode, data);
	}

}
