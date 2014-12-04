package com.justin.reader.util;

import java.io.BufferedReader;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;

import android.text.TextUtils;
import android.util.Log;
import android.webkit.WebView;

import com.justin.reader.R;

public class util {
	private static void LoadWebViewContent(WebView webView, String content) {

		webView.loadDataWithBaseURL(
				"file:///android_asset/"
						+ ReaderApp.getInstance().getString(
								R.string.text_reader_html_assets_folder) + "/",
				content,
				"text/html",
				ReaderApp.getInstance().getString(
						R.string.text_reader_html_encoding), null);
	}

	public static String readFle(String filelName) {
		Log.e("error", filelName);
		String content = "";
		try {
			InputStream in = ReaderApp
					.getInstance()
					.getResources()
					.getAssets()
					.open(ReaderApp.getInstance().getString(
							R.string.text_reader_html_assets_folder)
							+ "/" + filelName);
			byte[] temp = readInputStream(in);
			content = new String(temp);
		} catch (Exception e) {
			Log.e("error", e.toString());
		}
		return content;
	}

	public static void fillWebView(WebView webView, String fileName,
			boolean showLines, boolean wordwrap) {

		if (fileName == null || fileName.equals(""))
			return;
		File f = new File(fileName);
		if (!f.exists() || !f.isFile())
			return;
		String htmlContent = readFle(ReaderApp.getInstance().getString(
				R.string.text_reader_html_filename));

		String showLinesTag = ReaderApp.getInstance().getString(
				R.string.text_reader_html_tag_showlines);
		String showLineValue = showLinesTag.replace("##", "");

		String wordWrapTag = ReaderApp.getInstance().getString(
				R.string.text_reader_html_tag_wordwrap);
		String wordWrapValue = wordWrapTag.replace("##", "");
		String contentTag = ReaderApp.getInstance().getString(
				R.string.text_reader_html_tag_content);

		String content = ReadTxtFile(fileName);
		content = TextUtils.htmlEncode(content).replace("\n", "<br>");

		htmlContent = htmlContent
				.replace(showLinesTag, showLines ? showLineValue : "")
				.replace(wordWrapTag, wordwrap ? wordWrapValue : "")
				.replace(contentTag, content);
		String prefix = fileName.substring(fileName.lastIndexOf(".") + 1);

		LoadWebViewContent(webView, htmlContent);
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

			e.printStackTrace();
		}
		return outSteam.toByteArray();
	}

	public static String ReadTxtFile(String filePath) {

		StringBuilder fileContent = new StringBuilder();
		File file = new File(filePath);

		if (file.isDirectory() || !file.exists()) {
			Log.d("TestFile", "The File doesn't not exist." + filePath);
		}
		try {
			InputStream instream = new FileInputStream(file);
			if (instream != null) {
				InputStreamReader inputreader = new InputStreamReader(instream);
				BufferedReader buffreader = new BufferedReader(inputreader);
				String line = "";
				while ((line = buffreader.readLine()) != null) {
					fileContent.append(line + "\n");
				}
				instream.close();
			}
		} catch (java.io.FileNotFoundException e) {
			Log.d("TestFile", "The File doesn't not exist." + filePath);
		} catch (IOException e) {
			Log.d("TestFile", e.getMessage());
		}
	/*	if (fileContent.length() < 1)
			fileContent.append(" ");*/
		return fileContent.substring(1);
	}
}
