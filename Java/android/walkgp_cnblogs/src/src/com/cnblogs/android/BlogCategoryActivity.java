package com.cnblogs.android;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import com.cnblogs.android.core.Config;
import com.cnblogs.android.entity.BlogCategory;
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
import android.widget.TextView;
import android.widget.AdapterView.OnItemClickListener;

public class BlogCategoryActivity extends BaseActivity implements
		OnItemClickListener {

	Resources res;
	ListView listview;
	ImageButton blog_category_ok;
	BlogCategory selectedCategory;
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
				/*Intent data = new Intent();			 
				data.putExtra(Config.SELECTED_BLOG_CAGEGORY, selectedCategory);
				setResult(Config.REQUEST_BLOG_CAGEGORY, data);
				finish();*/
			}
		});

		categories = getCategories();

		listview = (ListView) findViewById(R.id.category_list);
		listview.setAdapter(new CategoryAdapter(this, categories));
		// 设置监听
		listview.setOnItemClickListener(this);

		
		Log.e("NetHelper", "______________读取数据Over ");
	}

	// item的点击监听时间
	@Override
	public void onItemClick(AdapterView<?> view, View arg1, int position,
			long arg3) {
		selectedCategory = categories.get(position);
		Intent intent = new Intent();		 
		Bundle bundle = new Bundle();
		bundle.putSerializable(Config.SELECTED_BLOG_CAGEGORY, selectedCategory);
		intent.putExtras(bundle); 
		setResult(Config.REQUEST_BLOG_CAGEGORY, intent);
		BlogCategoryActivity.this.finish();
	}

	public List<BlogCategory> getCategories() {
		List<BlogCategory> categories = new ArrayList<BlogCategory>();
		categories.add(new BlogCategory("808","首页","/","SiteHome","0"));
		categories.add(new BlogCategory("108698",".NET技术","/cate/108698/","TopSiteCategory","0"));
		categories.add(new BlogCategory("18156",".NET新手区","/cate/beginner/","SiteCategory","108698"));
		categories.add(new BlogCategory("108699","ASP.NET","/cate/aspnet/","SiteCategory","108698"));
		categories.add(new BlogCategory("108700","C#","/cate/csharp/","SiteCategory","108698"));
		categories.add(new BlogCategory("108716","WinForm","/cate/winform/","SiteCategory","108698"));
		categories.add(new BlogCategory("108717","Silverlight","/cate/silverlight/","SiteCategory","108698"));
		categories.add(new BlogCategory("108718","WCF","/cate/wcf/","SiteCategory","108698"));
		categories.add(new BlogCategory("108719","CLR","/cate/clr/","SiteCategory","108698"));
		categories.add(new BlogCategory("108720","WPF","/cate/wpf/","SiteCategory","108698"));
		categories.add(new BlogCategory("108728","XNA","/cate/xna/","SiteCategory","108698"));
		categories.add(new BlogCategory("108729","Visual Studio","/cate/vs2010/","SiteCategory","108698"));
		categories.add(new BlogCategory("108730","ASP.NET MVC","/cate/mvc/","SiteCategory","108698"));
		categories.add(new BlogCategory("108738","控件开发","/cate/control/","SiteCategory","108698"));
		categories.add(new BlogCategory("108739","Entity Framework","/cate/ef/","SiteCategory","108698"));
		categories.add(new BlogCategory("108745","WinRT/Metro","/cate/winrt_metro/","SiteCategory","108698"));
		categories.add(new BlogCategory("2","编程语言","/cate/2/","TopSiteCategory","0"));
		categories.add(new BlogCategory("106876","Java","/cate/java/","SiteCategory","2"));
		categories.add(new BlogCategory("106880","C++","/cate/cpp/","SiteCategory","2"));
		categories.add(new BlogCategory("106882","PHP","/cate/php/","SiteCategory","2"));
		categories.add(new BlogCategory("106877","Delphi","/cate/delphi/","SiteCategory","2"));
		categories.add(new BlogCategory("108696","Python","/cate/python/","SiteCategory","2"));
		categories.add(new BlogCategory("106894","Ruby","/cate/ruby/","SiteCategory","2"));
		categories.add(new BlogCategory("108735","C","/cate/c/","SiteCategory","2"));
		categories.add(new BlogCategory("108746","Erlang","/cate/erlang/","SiteCategory","2"));
		categories.add(new BlogCategory("108748","Go","/cate/go/","SiteCategory","2"));
		categories.add(new BlogCategory("108751","Swift","/cate/swift/","SiteCategory","2"));
		categories.add(new BlogCategory("108742","Verilog","/cate/verilog/","SiteCategory","2"));
		categories.add(new BlogCategory("108701","软件设计","/cate/108701/","TopSiteCategory","0"));
		categories.add(new BlogCategory("106892","架构设计","/cate/design/","SiteCategory","108701"));
		categories.add(new BlogCategory("108702","面向对象","/cate/108702/","SiteCategory","108701"));
		categories.add(new BlogCategory("106884","设计模式","/cate/dp/","SiteCategory","108701"));
		categories.add(new BlogCategory("108750","领域驱动设计","/cate/ddd/","SiteCategory","108701"));
		categories.add(new BlogCategory("108703","Web前端","/cate/108703/","TopSiteCategory","0"));
		categories.add(new BlogCategory("106883","Html/Css","/cate/web/","SiteCategory","108703"));
		categories.add(new BlogCategory("106893","JavaScript","/cate/javascript/","SiteCategory","108703"));
		categories.add(new BlogCategory("108731","jQuery","/cate/jquery/","SiteCategory","108703"));
		categories.add(new BlogCategory("108737","HTML5","/cate/html5/","SiteCategory","108703"));
		categories.add(new BlogCategory("108704","企业信息化","/cate/108704/","TopSiteCategory","0"));
		categories.add(new BlogCategory("78111","SharePoint","/cate/sharepoint/","SiteCategory","108704"));
		categories.add(new BlogCategory("50349","GIS技术","/cate/gis/","SiteCategory","108704"));
		categories.add(new BlogCategory("106878","SAP","/cate/sap/","SiteCategory","108704"));
		categories.add(new BlogCategory("108732","Oracle ERP","/cate/OracleERP/","SiteCategory","108704"));
		categories.add(new BlogCategory("108734","Dynamics CRM","/cate/dynamics/","SiteCategory","108704"));
		categories.add(new BlogCategory("108747","K2 BPM","/cate/k2/","SiteCategory","108704"));
		categories.add(new BlogCategory("108749","信息安全","/cate/infosec/","SiteCategory","108704"));
		categories.add(new BlogCategory("3","企业信息化其他","/cate/3/","SiteCategory","108704"));
		categories.add(new BlogCategory("108705","手机开发","/cate/108705/","TopSiteCategory","0"));
		categories.add(new BlogCategory("108706","Android开发","/cate/android/","SiteCategory","108705"));
		categories.add(new BlogCategory("108707","iOS开发","/cate/ios/","SiteCategory","108705"));
		categories.add(new BlogCategory("108736","Windows Phone","/cate/wp/","SiteCategory","108705"));
		categories.add(new BlogCategory("108708","Windows Mobile","/cate/wm/","SiteCategory","108705"));
		categories.add(new BlogCategory("106886","其他手机开发","/cate/mobile/","SiteCategory","108705"));
		categories.add(new BlogCategory("108709","软件工程","/cate/108709/","TopSiteCategory","0"));
		categories.add(new BlogCategory("108710","敏捷开发","/cate/agile/","SiteCategory","108709"));
		categories.add(new BlogCategory("106891","项目与团队管理","/cate/pm/","SiteCategory","108709"));
		categories.add(new BlogCategory("106889","软件工程其他","/cate/Engineering/","SiteCategory","108709"));
		categories.add(new BlogCategory("108712","数据库技术","/cate/108712/","TopSiteCategory","0"));
		categories.add(new BlogCategory("108713","SQL Server","/cate/sqlserver/","SiteCategory","108712"));
		categories.add(new BlogCategory("108714","Oracle","/cate/oracle/","SiteCategory","108712"));
		categories.add(new BlogCategory("108715","MySQL","/cate/mysql/","SiteCategory","108712"));
		categories.add(new BlogCategory("108743","NoSQL","/cate/nosql/","SiteCategory","108712"));
		categories.add(new BlogCategory("106881","其他数据库","/cate/database/","SiteCategory","108712"));
		categories.add(new BlogCategory("108724","操作系统","/cate/108724/","TopSiteCategory","0"));
		categories.add(new BlogCategory("108721","Windows 7","/cate/win7/","SiteCategory","108724"));
		categories.add(new BlogCategory("108725",">Windows Server","/cate/winserver/","SiteCategory","108724"));
		categories.add(new BlogCategory("108726","Linux","/cate/linux/","SiteCategory","108724"));
		categories.add(new BlogCategory("4","其他分类","/cate/4/","TopSiteCategory","0"));

		return categories;
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
