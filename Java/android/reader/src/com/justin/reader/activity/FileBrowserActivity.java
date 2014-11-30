package com.justin.reader.activity;

import com.justin.reader.R;
import com.justin.reader.controls.FileBrowser;
import com.justin.reader.controls.OnFileBrowserListener;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.KeyEvent;

public class FileBrowserActivity extends Activity implements
		OnFileBrowserListener {
	FileBrowser fileBrowser;

	@Override
	public void onFileItemClick(String filename) {
		setTitle(filename);
		Intent intent = new Intent(FileBrowserActivity.this,
				ImageViewerActivity.class);
		intent.putExtra("fileName", filename);
		setResult(RESULT_OK, intent); // intent为A传来的带有Bundle的intent，当然也可以自己定义新的Bundle
		finish();// 此处一定要调用finish()方法
	}

	@Override
	public void onDirItemClick(String path) {
		setTitle(path);
		
	}

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub		
		super.onCreate(savedInstanceState);		
		setContentView(R.layout.activity_file_browser);
		String fileName=getIntent().getBundleExtra("bd").getString("fileName");
		fileBrowser = (FileBrowser) findViewById(R.id.filebrowser);
		fileBrowser.setOnFileBrowserListener(this);
		fileBrowser.brower(fileName);
	}

	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event) {
		if (keyCode == KeyEvent.KEYCODE_BACK && event.getRepeatCount() == 0) {
			if (!fileBrowser.getCanUp())
				finish();
			fileBrowser.Up();
			return true;
		} else
			return super.onKeyDown(keyCode, event);
	}
}