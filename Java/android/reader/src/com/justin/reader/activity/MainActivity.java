package com.justin.reader.activity;

import com.justin.reader.R;
import com.justin.reader.controls.FileBrowser;
import com.justin.reader.controls.OnFileBrowserListener;
import com.justin.reader.util.util;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.webkit.WebView;
import android.widget.ImageView;
import android.widget.TextView;

public class MainActivity extends Activity implements
OnFileBrowserListener {  
	WebView webView;
	private boolean showLines = true;
	private boolean wordwrap = true;
	TextView txtFileName;
	String fileName;
	ImageView collapseImage;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
	 
		txtFileName = (TextView) findViewById(R.id.txtfilename);

		fileBrowser = (FileBrowser) findViewById(R.id.filebrowser);
		fileBrowser.setOnFileBrowserListener(this);

		webView = (WebView)findViewById(R.id.webView1);
		webView.getSettings().setJavaScriptEnabled(true);
		webView.requestFocus();

		collapseImage = (ImageView) findViewById(R.id.imagecollapse);
		collapseImage.setOnClickListener(new ImageView.OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				if (fileBrowser.getVisibility() == View.VISIBLE) {
					fileBrowser.setVisibility(View.GONE);
					collapseImage.setImageResource(R.drawable.open);
				} else {
					fileBrowser.setVisibility(View.VISIBLE);
					collapseImage.setImageResource(R.drawable.close);
				}
			}

		});
		fileName = txtFileName.getText() == null ? "" : txtFileName.getText()
				.toString();
		fileBrowser.brower(fileName);
		util.fillWebView(webView,fileName,showLines,wordwrap);
	}
	FileBrowser fileBrowser;

	@Override
	public void onFileItemClick(String filename) {

		/*setTitle(filename);
		Intent intent = new Intent(FileBrowserActivity.this,
				ImageViewerActivity.class);
		intent.putExtra("fileName", filename);
		setResult(RESULT_OK, intent); // intent为A传来的带有Bundle的intent，当然也可以自己定义新的Bundle
		finish();// 此处一定要调用finish()方法
		*/

		util.fillWebView(webView,filename,showLines,wordwrap);
		txtFileName.setText(filename);
	}

	@Override
	public void onDirItemClick(String path) {
		/*setTitle(path);*/
		txtFileName.setText(path);
	}

}