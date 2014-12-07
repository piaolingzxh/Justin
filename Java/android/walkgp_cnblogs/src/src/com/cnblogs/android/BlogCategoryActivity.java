package com.cnblogs.android;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import com.cnblogs.android.core.Config;
import com.cnblogs.android.enums.EnumActivityType;
import com.cnblogs.android.utility.BlogListHtmlParse;
import com.cnblogs.android.utility.NetHelper;

import android.app.ListActivity;
import android.content.Context;
import android.content.Intent;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.RadioButton;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.SimpleAdapter.ViewBinder;

public class BlogCategoryActivity extends BaseActivity implements
		OnItemClickListener {

	Resources res;
	ListView listview;
	ImageButton blog_category_ok;
	String selectedCategory;
	List<BlogCategory> categories;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_blog_category);
		res = this.getResources();

		InitialControls();
	}

	/*
	 * 初始化控件
	 */
	void InitialControls() {
		Button btnBack = (Button) findViewById(R.id.btn_back);
		btnBack.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				finish();
			}
		});

		ImageButton blog_category_ok = (ImageButton) findViewById(R.id.blog_category_ok);
		blog_category_ok.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				Intent data = new Intent();
				selectedCategory = "test";
				data.putExtra(Config.BLOG_CAGEGORY, selectedCategory);
				setResult(Config.REQUEST_BLOG_CAGEGORY, data);
				finish();
			}
		});

		categories = getCategories();

		listview = (ListView) findViewById(R.id.category_list);
		listview.setAdapter(new CategoryAdapter(this, categories));
		// 设置监听
		listview.setOnItemClickListener(this);

		String blog_list_html = NetHelper
				.GetContentFromUrl("http://www.cnblogs.com/mvc/AggSite/PostList.aspx?CategoryType=SiteCategory&ParentCategoryId=108698&CategoryId=108699&PageIndex=7&ItemListActionName=PostList");
		List<BlogCategory> categories = getCategories();
		// BlogListHtmlParse.JiexiBlogList(blog_list_html);
		for (BlogCategory category : categories) {

			String categoryInfoHtml = NetHelper
					.GetContentFromUrl("http://www.cnblogs.com" + category.URL);
			BlogListHtmlParse.JiexiCategoryInfo(categoryInfoHtml, category);
		}
		StringBuilder sb = new StringBuilder();
		for (BlogCategory category : categories) {

			String categoryItemString = "<item url=\"{url}\" name=\"{text}\" id=\"{id}\" tp=\"{tp}\" pid=\"{pid}\"></item>";
			categoryItemString = categoryItemString
					.replace("{url}", category.URL)
					.replace("{text}", category.Name)
					.replace("{id}", category.CategoryId)
					.replace("{tp}", category.CategoryType)
					.replace("{pid}", category.ParentCategoryId);
			sb.append(categoryItemString);
		}
		try {
			BlogListHtmlParse.writeFileSdcard("/sdcard/Android.txt",
					sb.toString());
		} catch (Exception e) {
			Log.e("NetHelper", "______________读取数据失败 " + e.toString());
		}
		Log.e("NetHelper", "______________读取数据Over ");
	}

	// item的点击监听时间
	@Override
	public void onItemClick(AdapterView<?> view, View arg1, int position,
			long arg3) {
		selectedCategory = categories.get(position).URL;
		Intent data = new Intent();

		data.putExtra(Config.BLOG_CAGEGORY, selectedCategory);
		setResult(Config.REQUEST_BLOG_CAGEGORY, data);
		BlogCategoryActivity.this.finish();
	}

	public List<BlogCategory> getCategories() {
		List<BlogCategory> categories = new ArrayList<BlogCategory>();
		categories.add(new BlogCategory("/", "首页"));
		categories.add(new BlogCategory("/cate/108698/", ".NET技术"));
		categories.add(new BlogCategory("/cate/beginner/", ".NET新手区"));
		categories.add(new BlogCategory("/cate/aspnet/", "ASP.NET"));
		categories.add(new BlogCategory("/cate/csharp/", "C#"));
		categories.add(new BlogCategory("/cate/winform/", "WinForm"));
		categories.add(new BlogCategory("/cate/silverlight/", "Silverlight"));
		categories.add(new BlogCategory("/cate/wcf/", "WCF"));
		categories.add(new BlogCategory("/cate/clr/", "CLR"));
		categories.add(new BlogCategory("/cate/wpf/", "WPF"));
		categories.add(new BlogCategory("/cate/xna/", "XNA"));
		categories.add(new BlogCategory("/cate/vs2010/", "Visual Studio"));
		categories.add(new BlogCategory("/cate/mvc/", "ASP.NET MVC"));
		categories.add(new BlogCategory("/cate/control/", "控件开发"));
		categories.add(new BlogCategory("/cate/ef/", "Entity Framework"));
		categories.add(new BlogCategory("/cate/winrt_metro/", "WinRT/Metro"));
		categories.add(new BlogCategory("/cate/2/", "编程语言"));
		categories.add(new BlogCategory("/cate/java/", "Java"));
		categories.add(new BlogCategory("/cate/cpp/", "C++"));
		categories.add(new BlogCategory("/cate/php/", "PHP"));
		categories.add(new BlogCategory("/cate/delphi/", "Delphi"));
		categories.add(new BlogCategory("/cate/python/", "Python"));
		categories.add(new BlogCategory("/cate/ruby/", "Ruby"));
		categories.add(new BlogCategory("/cate/c/", "C"));
		categories.add(new BlogCategory("/cate/erlang/", "Erlang"));
		categories.add(new BlogCategory("/cate/go/", "Go"));
		categories.add(new BlogCategory("/cate/swift/", "Swift"));
		categories.add(new BlogCategory("/cate/verilog/", "Verilog"));
		categories.add(new BlogCategory("/cate/108701/", "软件设计"));
		categories.add(new BlogCategory("/cate/design/", "架构设计"));
		categories.add(new BlogCategory("/cate/108702/", "面向对象"));
		categories.add(new BlogCategory("/cate/dp/", "设计模式"));
		categories.add(new BlogCategory("/cate/ddd/", "领域驱动设计"));
		categories.add(new BlogCategory("/cate/108703/", "Web前端"));
		categories.add(new BlogCategory("/cate/web/", "Html/Css"));
		categories.add(new BlogCategory("/cate/javascript/", "JavaScript"));
		categories.add(new BlogCategory("/cate/jquery/", "jQuery"));
		categories.add(new BlogCategory("/cate/html5/", "HTML5"));
		categories.add(new BlogCategory("/cate/108704/", "企业信息化"));
		categories.add(new BlogCategory("/cate/sharepoint/", "SharePoint"));
		categories.add(new BlogCategory("/cate/gis/", "GIS技术"));
		categories.add(new BlogCategory("/cate/sap/", "SAP"));
		categories.add(new BlogCategory("/cate/OracleERP/", "Oracle ERP"));
		categories.add(new BlogCategory("/cate/dynamics/", "Dynamics CRM"));
		categories.add(new BlogCategory("/cate/k2/", "K2 BPM"));
		categories.add(new BlogCategory("/cate/infosec/", "信息安全"));
		categories.add(new BlogCategory("/cate/3/", "企业信息化其他"));
		categories.add(new BlogCategory("/cate/108705/", "手机开发"));
		categories.add(new BlogCategory("/cate/android/", "Android开发"));
		categories.add(new BlogCategory("/cate/ios/", "iOS开发"));
		categories.add(new BlogCategory("/cate/wp/", "Windows Phone"));
		categories.add(new BlogCategory("/cate/wm/", "Windows Mobile"));
		categories.add(new BlogCategory("/cate/mobile/", "其他手机开发"));
		categories.add(new BlogCategory("/cate/108709/", "软件工程"));
		categories.add(new BlogCategory("/cate/agile/", "敏捷开发"));
		categories.add(new BlogCategory("/cate/pm/", "项目与团队管理"));
		categories.add(new BlogCategory("/cate/Engineering/", "软件工程其他"));
		categories.add(new BlogCategory("/cate/108712/", "数据库技术"));
		categories.add(new BlogCategory("/cate/sqlserver/", "SQL Server"));
		categories.add(new BlogCategory("/cate/oracle/", "Oracle"));
		categories.add(new BlogCategory("/cate/mysql/", "MySQL"));
		categories.add(new BlogCategory("/cate/nosql/", "NoSQL"));
		categories.add(new BlogCategory("/cate/database/", "其他数据库"));
		categories.add(new BlogCategory("/cate/108724/", "操作系统"));
		categories.add(new BlogCategory("/cate/win7/", "Windows 7"));
		categories.add(new BlogCategory("/cate/winserver/", ">Windows Server"));
		categories.add(new BlogCategory("/cate/linux/", "Linux"));
		categories.add(new BlogCategory("/cate/4/", "其他分类"));
		return categories;
	}

	public class BlogCategory {
		public BlogCategory() {
		}

		public BlogCategory(String url, String name) {
			URL = url;
			Name = name;
		}

		public String URL;
		public String Name;
		public int Count;
		// var aggSiteModel =
		// {"CategoryType":"SiteHome","ParentCategoryId":0,"CategoryId":808,"PageIndex":1,"ItemListActionName":"PostList"};
		// var aggSiteModel =
		// {"CategoryType":"SiteCategory","ParentCategoryId":108705,"CategoryId":108706,"PageIndex":1,"ItemListActionName":"PostList"};
		// var aggSiteModel =
		// {"CategoryType":"SiteCategory","ParentCategoryId":108712,"CategoryId":108713,"PageIndex":1,"ItemListActionName":"PostList"};

		public String CategoryType;
		public String ParentCategoryId;
		public String CategoryId;
	}

	public class CategoryAdapter extends BaseAdapter {

		private Context context;
		private List<BlogCategory> lists;
		private LayoutInflater layoutInflater;
		ImageView img;
		TextView tv1;
		ImageView imageIsSelected;

		/**
		 * 构造函数，进行初始化
		 * 
		 * @param context
		 * @param lists
		 */
		CategoryAdapter(Context context, List<BlogCategory> lists) {
			this.context = context;
			this.lists = lists;
			layoutInflater = LayoutInflater.from(this.context);
		}

		// 获得长度，一般返回数据的长度即可
		@Override
		public int getCount() {
			return lists.size();
		}

		@Override
		public Object getItem(int position) {
			return lists.get(position);
		}

		@Override
		public long getItemId(int position) {
			return position;
		}

		/**
		 * 最重要的方法，每一个item生成的时候，都会执行这个方法，在这个方法中实现数据与item中每个控件的绑定
		 */
		@Override
		public View getView(int position, View convertView, ViewGroup parent) {

			if (convertView == null) {
				convertView = layoutInflater.inflate(
						R.layout.category_list_item, null);
			}

			img = (ImageView) convertView.findViewById(R.id.category_image);
			tv1 = (TextView) convertView.findViewById(R.id.categroy_text);
			imageIsSelected = (ImageView) convertView
					.findViewById(R.id.category_selected);

			// img.setBackgroundResource(lists.get(position).getPicture());
			tv1.setText(lists.get(position).Name);

			return convertView;
		}

	}
}
