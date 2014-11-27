package com.example.androidtest;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import android.webkit.WebView;

public class MainActivity extends Activity {

	private WebView webView;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);

		webView = (WebView) this.findViewById(R.id.wv);
		webView.getSettings().setJavaScriptEnabled(true);

		webView.requestFocus();
		// webView.loadUrl("file:///android_asset/index1.html");// 加载html文件

		String htmlContent = readFle("index.html");

		htmlContent = htmlContent.replace("##code##", readFle("SqlHelper.cs"));
		LoadWebViewContent(webView, htmlContent);
	}

	private void LoadWebViewContent(WebView webView, String content) {
		webView.loadDataWithBaseURL("file:///android_asset/", content,
				"text/html", "utf-8", null);
	}

	public String readFle(String filelName) {
		String content = "";
		try {
			InputStream in = getResources().getAssets().open(filelName);
			byte[] temp = readInputStream(in);
			content = new String(temp);
		} catch (Exception e) {
			Log.e("error", e.toString());
		}
		return content;
	}

	public static byte[] readInputStream(InputStream inStream) {

		ByteArrayOutputStream outSteam = new ByteArrayOutputStream();
		try {
			byte[] buffer = new byte[4096];
			int len = 0;

			while ((len = inStream.read(buffer)) != -1) {
				outSteam.write(buffer, 0, len);
			}
			outSteam.close();
			inStream.close();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return outSteam.toByteArray();

	}
}
