package com.cnblogs.android;

import java.util.Hashtable;
import com.cnblogs.android.utility.PreferencesHelper;
import android.app.TabActivity;
import android.content.Intent;
import android.content.res.Resources;
import android.os.Bundle;
import android.widget.CompoundButton;
import android.widget.RadioButton;
import android.widget.TabHost;
import android.widget.CompoundButton.OnCheckedChangeListener;

/**
 * 主Activity，放置5个Tab
 * 
 * @author walkingp
 * @date:2011-12
 * 
 */
public class MainActivity extends TabActivity implements
		OnCheckedChangeListener {

	private TabHost tabHost;
	Hashtable<String, RadioButton> radios = new Hashtable<String, RadioButton>();

	private RadioButton rbBlog;
	private RadioButton rbNews;
	private RadioButton rbRss;
	private RadioButton rbSearch;
	private RadioButton rbMore;

	/* public String whichTab = ""; */
	Resources res;// 资源

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.main);
		res = this.getResources();
		InitialRadios();
		InitialTab();
		InitialSelectedTab();
	}

	/**
	 * 初始化单选按钮
	 */
	private void InitialRadios() {

		rbBlog = (RadioButton) findViewById(R.id.TabBlog);
		rbBlog.setOnCheckedChangeListener(this);
		radios.put(rbBlog.getTag().toString(), rbBlog);

		rbNews = (RadioButton) findViewById(R.id.TabNews);
		rbNews.setOnCheckedChangeListener(this);
		radios.put(rbNews.getTag().toString(), rbNews);

		rbRss = (RadioButton) findViewById(R.id.TabRss);
		rbRss.setOnCheckedChangeListener(this);
		radios.put(rbRss.getTag().toString(), rbRss);

		rbSearch = (RadioButton) findViewById(R.id.TabSearch);
		rbSearch.setOnCheckedChangeListener(this);
		radios.put(rbSearch.getTag().toString(), rbSearch);

		rbMore = (RadioButton) findViewById(R.id.TabMore);
		rbMore.setOnCheckedChangeListener(this);
		radios.put(rbMore.getTag().toString(), rbMore);
	}

	/**
	 * 初始化Tab
	 */
	private void InitialTab() {
		tabHost = this.getTabHost();

		tabHost.addTab(buildTabSpec(rbBlog.getTag().toString(),
				R.string.main_home, R.drawable.icon, new Intent(this,
						BlogActivity.class)));
		tabHost.addTab(buildTabSpec(rbNews.getTag().toString(),
				R.string.main_news, R.drawable.icon, new Intent(this,
						NewsActivity.class)));
		tabHost.addTab(buildTabSpec(rbRss.getTag().toString(),
				R.string.main_rss, R.drawable.icon, new Intent(this,
						MyRssActivity.class)));
		tabHost.addTab(buildTabSpec(rbSearch.getTag().toString(),
				R.string.main_search, R.drawable.icon, new Intent(this,
						SearchActivity.class)));
		tabHost.addTab(buildTabSpec(rbMore.getTag().toString(),
				R.string.main_more, R.drawable.icon, new Intent(this,
						MoreActivity.class)));
	}

	/**
	 * 设置默认选中Tab
	 */
	private void InitialSelectedTab() {

		String selectTab = PreferencesHelper.getStroedTabName(rbBlog.getTag()
				.toString());
		if (!radios.containsKey(selectTab)) {
			selectTab = rbBlog.getTag().toString();
		}

		radios.get(selectTab).setChecked(true);
	}

	private TabHost.TabSpec buildTabSpec(String tag, int resLabel, int resIcon,
			final Intent content) {
		return tabHost
				.newTabSpec(tag)
				.setIndicator(getString(resLabel),
						getResources().getDrawable(resIcon))
				.setContent(content);
	}

	public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
		if (!isChecked) {
			return;
		}
		RadioButton checkedButton = (RadioButton) findViewById(buttonView
				.getId());
		tabHost.setCurrentTabByTag(checkedButton.getTag().toString());
	}

	// 存储关闭时的tab
	protected void onDestroy() {
		PreferencesHelper.setStoredTabName(tabHost.getCurrentTabTag());
		super.onDestroy();
	}

}