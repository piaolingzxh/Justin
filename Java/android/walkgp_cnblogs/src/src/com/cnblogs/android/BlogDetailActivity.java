package com.cnblogs.android;

import java.io.InputStream;

import com.cnblogs.android.cache.ImageCacher;
import com.cnblogs.android.core.BlogHelper;
import com.cnblogs.android.core.Config;
import com.cnblogs.android.core.FavListHelper;
import com.cnblogs.android.core.UserHelper;
import com.cnblogs.android.dal.BlogDalHelper;
import com.cnblogs.android.entity.Blog;
import com.cnblogs.android.entity.FavList;
import com.cnblogs.android.enums.EnumResultType;
import com.cnblogs.android.utility.AppUtil;
import com.cnblogs.android.utility.NetHelper;
import com.cnblogs.android.utility.PreferencesHelper;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.Resources;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.GestureDetector;
import android.view.ContextMenu.ContextMenuInfo;
import android.view.GestureDetector.OnGestureListener;
import android.view.ContextMenu;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import android.view.WindowManager;
import android.view.View.OnClickListener;
import android.webkit.WebSettings;
import android.webkit.WebSettings.LayoutAlgorithm;
import android.webkit.WebView;
import android.webkit.WebSettings.TextSize;
import android.widget.Button;
import android.widget.ProgressBar;
import android.widget.RelativeLayout;
import android.widget.SeekBar;
import android.widget.TextView;
import android.widget.Toast;

/**
 * 博客详细内容
 * 
 * @author walkingp
 * @date:2011-12
 * 
 */
public class BlogDetailActivity extends BaseActivity implements
		OnGestureListener {

	private Blog blog;

	static final int MENU_FORMAT_HTML = Menu.FIRST;// 格式化阅读
	static final int MENU_READ_MODE = Menu.FIRST + 1;// 切换阅读模式

	final String mimeType = "text/html";
	final String encoding = "utf-8";
	// 顶部
	RelativeLayout rl_blog_detail;// 头部导航
	private Button comment_btn;// 评论按钮
	private Button blog_button_back;// 返回

	WebView webView;
	ProgressBar blogBody_progressBar;

	private GestureDetector gestureScanner;// 手势

	Resources res;// 资源
	TextView tvSeekBar;// SeekBar显示文本框
	SeekBar seekBar;// SeekBar

	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		// 防止休眠
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON,
				WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
		this.setContentView(R.layout.blog_detail);

		res = this.getResources();

		InitialData();
	}

	// 长按菜单
	public void onCreateContextMenu(ContextMenu menu, View v,
			ContextMenuInfo menuInfo) {
		if (v.getId() == R.id.blog_body_webview_content) {
			menu.setHeaderTitle("请选择操作");
			menu.add(0, MENU_FORMAT_HTML, 0, "查看内容");
			menu.add(0, MENU_READ_MODE, 1, "切换到模式");
		}
	}

	/**
	 * 菜单
	 */
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		MenuInflater inflater = getMenuInflater();
		inflater.inflate(R.menu.blog_detail_menu, menu);
		return super.onCreateOptionsMenu(menu);
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
		case R.id.menu_blog_back:// 返回列表
			BlogDetailActivity.this.setResult(0, getIntent());
			BlogDetailActivity.this.finish();
			break;
		case R.id.menu_blog_comment:// 查看评论
			RedirectCommentActivity();
			break;
		case R.id.menu_blog_share:// 分享
			Intent intent = new Intent(Intent.ACTION_SEND);
			intent.setType("text/plain");
			intent.putExtra(Intent.EXTRA_SUBJECT, blog.GetBlogTitle());
			String shareContent = "《" + blog.GetBlogTitle() + "》,作者："
					+ blog.GetAuthor() + "，原文链接：" + blog.GetBlogUrl() + " 分享自："
					+ res.getString(R.string.app_name) + "Android客户端("
					+ res.getString(R.string.app_homepage) + ")";
			intent.putExtra(Intent.EXTRA_TEXT, shareContent);
			startActivity(Intent.createChooser(intent, blog.GetBlogTitle()));
			break;
		case R.id.menu_blog_add_fav:// 添加收藏
			new AddFavTask().execute(blog.GetBlogId());
			break;
		case R.id.menu_blog_author:// 博主
			RedirectAuthorActivity();
			break;
		case R.id.menu_blog_browser:// 查看网页
			Uri blogUri = Uri.parse(blog.GetBlogUrl());
			Intent it = new Intent(Intent.ACTION_VIEW, blogUri);
			startActivity(it);
			break;
		}
		return super.onOptionsItemSelected(item);
	}

	/**
	 * 操作数据库
	 */
	private void MarkAsReaded() {
		// 更新为已读
		BlogDalHelper helper = new BlogDalHelper(getApplicationContext());
		helper.MarkAsReaded(blog.GetBlogId());
		// 广播
		Intent intent = new Intent();
		Bundle bundle = new Bundle();
		bundle.putIntArray("blogIdArray", new int[] { blog.GetBlogId() });
		intent.putExtras(bundle);
		intent.setAction("android.cnblogs.com.update_bloglist");
		this.sendBroadcast(intent);
	}

	/**
	 * 初始化
	 */
	private void InitialData() {
		// 传递过来的值

		blog = (Blog) getIntent().getSerializableExtra("blog");

		// 头部
		rl_blog_detail = (RelativeLayout) findViewById(R.id.rl_blog_detail);
		// 返回
		blog_button_back = (Button) findViewById(R.id.blog_button_back);
		blog_button_back.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
				BlogDetailActivity.this.finish();
			}
		});
		// 打开评论
		comment_btn = (Button) findViewById(R.id.blog_comment_btn);
		String commentsCountString = (blog.GetCommentNum() == 0) ? "暂无评论"
				: blog.GetCommentNum() + "条评论";
		comment_btn.setText(commentsCountString);
		comment_btn.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
				RedirectCommentActivity();
			}
		});

		// 双击全屏
		rl_blog_detail.setOnTouchListener(new OnTouchListener() {
			@Override
			public boolean onTouch(View v, MotionEvent event) {
				return gestureScanner.onTouchEvent(event);
			}
		});

		try {
			// 1、
			blogBody_progressBar = (ProgressBar) findViewById(R.id.blogBody_progressBar);
			// 2、初始是否全屏
			if (allowFullScreen) {
				setFullScreen();
			}
			// 3、
			webView = (WebView) findViewById(R.id.blog_body_webview_content);
			webView.getSettings().setDefaultTextEncodingName("utf-8");// 避免中文乱码
			webView.addJavascriptInterface(this, "javatojs");
			webView.setSelected(true);
			webView.setScrollBarStyle(0);

			WebSettings webSetting = webView.getSettings();
			webSetting.setJavaScriptEnabled(true);
			webSetting.setPluginsEnabled(true);
			webSetting.setNeedInitialFocus(false);
			webSetting.setSupportZoom(true);
			webSetting.setLayoutAlgorithm(LayoutAlgorithm.SINGLE_COLUMN);
			webSetting.setTextSize(textSize);
			webSetting.setCacheMode(WebSettings.LOAD_DEFAULT
					| WebSettings.LOAD_CACHE_ELSE_NETWORK);

			webView.getSettings().setLoadsImagesAutomatically(isAutoLoadImage);
			// 双击全屏
			webView.setOnTouchListener(new OnTouchListener() {
				@Override
				public boolean onTouch(View v, MotionEvent event) {
					return gestureScanner.onTouchEvent(event);
				}
			});
			/*	int scalePercent = 110;
				// 上一次保存的缩放比例
				float webviewScale = sharePreferencesSettings.getFloat(
						res.getString(R.string.preferences_webview_zoom_scale),
						(float) 1.1);
				scalePercent = (int) (webviewScale * 100);
				webView.setInitialScale(1);*/

			// 4、
			String url = Config.URL_GET_BLOG_DETAIL.replace("{0}",
					String.valueOf(blog.GetBlogId()));// 网址
			new PageTask().execute(url);

		} catch (Exception ex) {
			Toast.makeText(getApplicationContext(), R.string.sys_error,
					Toast.LENGTH_SHORT).show();
		}

		// 监听屏幕动作事件 全屏
		gestureScanner = new GestureDetector(this);
		gestureScanner.setIsLongpressEnabled(true);
		gestureScanner
				.setOnDoubleTapListener(new GestureDetector.OnDoubleTapListener() {
					public boolean onDoubleTap(MotionEvent e) {
						if (allowFullScreen) {
							setFullScreen();
						} else {
							quitFullScreen();
						}
						return false;
					}

					public boolean onDoubleTapEvent(MotionEvent e) {
						return false;
					}

					public boolean onSingleTapConfirmed(MotionEvent e) {
						return false;
					}
				});
	}

	/**
	 * 保存缩放比例
	 */
	public void onDestroy() {
		/*float webviewScale = webView.getScale();
		sharePreferencesSettings
				.edit()
				.putFloat(
						res.getString(R.string.preferences_webview_zoom_scale),
						webviewScale).commit();*/
		super.onDestroy();
	}

	/**
	 * 打开评论
	 * 
	 */
	private void RedirectCommentActivity() {
		// 还没有评论
		if (blog.GetCommentNum() == 0) {
			Toast.makeText(getApplicationContext(), R.string.sys_empty_comment,
					Toast.LENGTH_SHORT).show();
			return;
		}
		Intent intent = new Intent();
		intent.setClass(BlogDetailActivity.this, CommentActivity.class);
		Bundle bundle = new Bundle();
		bundle.putInt("contentId", blog.GetBlogId());
		bundle.putInt("commentType", 0);// Comment.EnumCommentType.News.ordinal());
		bundle.putString("title", blog.GetBlogTitle());
		bundle.putString("url", blog.GetBlogUrl());

		intent.putExtras(bundle);

		startActivityForResult(intent, 0);
	}

	/**
	 * 跳转到博主
	 */
	private void RedirectAuthorActivity() {
		String userName = UserHelper.GetBlogUrlName(blog.GetBlogUrl());// 主页用户名
		if (userName.equals("")) {
			Toast.makeText(getApplicationContext(), R.string.sys_no_author,
					Toast.LENGTH_SHORT).show();
			return;
		}
		Intent intent = new Intent();
		intent.setClass(BlogDetailActivity.this, AuthorBlogActivity.class);
		Bundle bundle = new Bundle();
		bundle.putString("author", userName);// 用户名
		bundle.putString("blogName", blog.GetAuthor());// 博客标题

		intent.putExtras(bundle);

		startActivityForResult(intent, 0);
	}

	/**
	 * 加载内容
	 * 
	 * @param webView
	 * @param content
	 */
	private void LoadWebViewContent(WebView webView, String content) {
		webView.loadDataWithBaseURL(Config.LOCAL_PATH, content, "text/html",
				Config.ENCODE_TYPE, null);
	}

	/**
	 * 多线程启动
	 * 
	 * @author walkingp
	 * 
	 */
	public class PageTask extends AsyncTask<String, Integer, String> {
		// 可变长的输入参数，与AsyncTask.exucute()对应
		@Override
		protected String doInBackground(String... params) {

			try {
				String _blogContent = BlogHelper.GetBlogById(blog.GetBlogId(),
						getApplicationContext());
				// 下载图片（只有本地完整保存图片时才下载）

				Context context = getApplicationContext();
				BlogDalHelper helper = new BlogDalHelper(context);
				Blog entity = helper.GetBlogEntity(blog.GetBlogId());
				boolean isNetworkAvailable = NetHelper
						.networkIsAvailable(getApplicationContext());
				if (isNetworkAvailable
						&& (entity == null || !entity.GetIsFullText())) {
					ImageCacher imageCacher = new ImageCacher(
							getApplicationContext());
					imageCacher.DownloadHtmlImage(
							ImageCacher.EnumImageType.Blog, _blogContent);

					_blogContent = ImageCacher.FormatLocalHtmlWithImg(
							ImageCacher.EnumImageType.Blog, _blogContent);
				}

				return _blogContent;
			} catch (Exception e) {
				e.printStackTrace();
			}

			return "";
		}

		@Override
		protected void onCancelled() {
			super.onCancelled();
		}

		/**
		 * 加载内容
		 */
		@Override
		protected void onPostExecute(String _blogContent) {
			String htmlContent = "";
			try {
				InputStream in = getAssets().open("NewsDetail.html");
				byte[] temp = NetHelper.readInputStream(in);
				htmlContent = new String(temp);
			} catch (Exception e) {
				Log.e("error", e.toString());
			}

			_blogContent = AppUtil.FormatContent(getApplicationContext(),
					_blogContent);
			String publishTime = blog.GetAddTime() == null ? "" : blog
					.GetAddTime().toString();
			htmlContent = htmlContent
					.replace("#author#", blog.GetAuthor())

					.replace("#diggs#", Integer.toString(blog.GetDiggsNum()))
					.replace("#views#", Integer.toString(blog.GetViewNum()))
					.replace("#comments#",
							Integer.toString(blog.GetCommentNum()))
					.replace("#published#",
							AppUtil.ParseDateToString(blog.GetAddTime()))
					.replace(
							"#updated#",
							blog.GetUpdateTime() == null ? AppUtil
									.ParseDateToString(blog.GetAddTime())
									: AppUtil.ParseDateToString(blog
											.GetUpdateTime()))
					.replace("#blogurl#",
							blog.GetBlogUrl() == null ? "#" : blog.GetBlogUrl())
					.replace(
							"#authorurl#",
							blog.GetAuthorUrl() == null ? "#" : blog
									.GetAuthorUrl())
					.replace("#title#", blog.GetBlogTitle())
					.replace("#content#", _blogContent);

			LoadWebViewContent(webView, htmlContent);
			blogBody_progressBar.setVisibility(View.GONE);
			if (!_blogContent.equals("")) {
				// 更新为已读
				MarkAsReaded();
			}
		}

		@Override
		protected void onPreExecute() {
			blogBody_progressBar.setVisibility(View.VISIBLE);
		}

		@Override
		protected void onProgressUpdate(Integer... values) {
		}
	}

	/**
	 * 添加收藏
	 * 
	 */
	public class AddFavTask extends
			AsyncTask<Integer, String, EnumResultType.EnumActionResultType> {
		int contentId;

		@Override
		protected EnumResultType.EnumActionResultType doInBackground(
				Integer... params) {
			contentId = params[0];
			EnumResultType.EnumActionResultType result = FavListHelper.AddFav(
					contentId, FavList.EnumContentType.Blog,
					getApplicationContext());
			return result;
		}

		@Override
		protected void onPostExecute(EnumResultType.EnumActionResultType result) {
			if (result.equals(EnumResultType.EnumActionResultType.Succ)) {// 成功
				// 广播
				Intent intent = new Intent();
				Bundle bundle = new Bundle();
				bundle.putInt("contentId", contentId);
				bundle.putInt("contentType",
						FavList.EnumContentType.Blog.ordinal());
				bundle.putBoolean("isfav", true);
				intent.putExtras(bundle);
				intent.setAction("android.cnblogs.com.update_favlist");
				sendBroadcast(intent);
				Toast.makeText(getApplicationContext(), R.string.fav_succ,
						Toast.LENGTH_SHORT).show();
			} else if (result.equals(EnumResultType.EnumActionResultType.Exist)) {
				Toast.makeText(getApplicationContext(), R.string.sys_fav_exist,
						Toast.LENGTH_SHORT).show();
			} else {
				Toast.makeText(getApplicationContext(), R.string.fav_fail,
						Toast.LENGTH_SHORT).show();
			}
		}
	}

	// 实现OnGestureListener接口开始

	/*
	 全屏
	 */
	private void setFullScreen() {
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
				WindowManager.LayoutParams.FLAG_FULLSCREEN);
		// 隐藏导航
		rl_blog_detail.setVisibility(View.GONE);
	}

	/*
	 退出全屏
	 */
	private void quitFullScreen() {
		final WindowManager.LayoutParams attrs = getWindow().getAttributes();
		attrs.flags &= (~WindowManager.LayoutParams.FLAG_FULLSCREEN);
		getWindow().setAttributes(attrs);
		getWindow()
				.clearFlags(WindowManager.LayoutParams.FLAG_LAYOUT_NO_LIMITS);
		// 显示导航
		rl_blog_detail.setVisibility(View.VISIBLE);
	}

	@Override
	public boolean onDown(MotionEvent e) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public void onShowPress(MotionEvent e) {

	}

	@Override
	public boolean onSingleTapUp(MotionEvent e) {
		return false;
	}

	@Override
	public boolean onScroll(MotionEvent e1, MotionEvent e2, float distanceX,
			float distanceY) {
		return false;
	}

	@Override
	public void onLongPress(MotionEvent e) {
		// TODO Auto-generated method stub

	}

	@Override
	public boolean onFling(MotionEvent e1, MotionEvent e2, float velocityX,
			float velocityY) {
		// TODO Auto-generated method stub
		return false;
	}
	// 实现OnGestureListener接口结束
}
