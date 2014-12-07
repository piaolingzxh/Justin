package com.cnblogs.android.entity;

import java.io.Serializable;

public class BlogCategory implements Serializable{
	public BlogCategory() {
	}

	public BlogCategory(String id, String name,String url,String tp,String pid) {
		CategoryId=id; 
		
		Name = name;
		URL = url;
		CategoryType=tp;
		ParentCategoryId=pid;
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
