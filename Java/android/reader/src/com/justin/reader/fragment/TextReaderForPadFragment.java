package com.justin.reader.fragment;

import java.io.BufferedReader;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;

import org.apache.commons.lang.StringEscapeUtils;

import com.justin.reader.R;
import com.justin.reader.activity.FileBrowserActivity;
import com.justin.reader.activity.ImageViewerActivity;
import com.justin.reader.activity.MainActivity;
import com.justin.reader.controls.FileBrowser;
import com.justin.reader.controls.OnFileBrowserListener;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebView;

import android.widget.ImageView;
import android.widget.TextView;

/**
 * A simple {@link Fragment} subclass. Activities that contain this fragment
 * must implement the
 * {@link TextReaderForPadFragment.OnFragmentInteractionListener} interface to
 * handle interaction events. Use the
 * {@link TextReaderForPadFragment#newInstance} factory method to create an
 * instance of this fragment.
 * 
 */
public class TextReaderForPadFragment extends Fragment implements
		OnFileBrowserListener {
	// TODO: Rename parameter arguments, choose names that match
	// the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
	private static final String ARG_PARAM1 = "param1";
	private static final String ARG_PARAM2 = "param2";

	// TODO: Rename and change types of parameters
	private String mParam1;
	private String mParam2;
	WebView webView;
	private boolean showLines = true;
	private boolean wordwrap = true;
	private OnFragmentInteractionListener mListener;
	TextView txtFileName;
	String fileName;
	ImageView collapseImage;

	public TextReaderForPadFragment() {
		// Required empty public constructor
	}

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		if (getArguments() != null) {
			mParam1 = getArguments().getString(ARG_PARAM1);
			mParam2 = getArguments().getString(ARG_PARAM2);
		}
	}

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// Inflate the layout for this fragment
		View rootView = inflater.inflate(R.layout.fragment_text_reader_for_pad,
				container, false);
		txtFileName = (TextView) rootView.findViewById(R.id.txtfilename);

		fileBrowser = (FileBrowser) rootView.findViewById(R.id.filebrowser);
		fileBrowser.setOnFileBrowserListener(this);

		webView = (WebView) rootView.findViewById(R.id.webView1);
		webView.getSettings().setJavaScriptEnabled(true);
		webView.requestFocus();

		collapseImage = (ImageView) rootView.findViewById(R.id.imagecollapse);
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
		fileName = txtFileName.getText()==null?"":txtFileName.getText().toString();
		fileBrowser.brower(fileName);
		fillWebView(fileName);
		return rootView;
	}

	// TODO: Rename method, update argument and hook method into UI event
	public void onButtonPressed(Uri uri) {
		if (mListener != null) {
			mListener.onFragmentInteraction(uri);
		}
	}

	@Override
	public void onAttach(Activity activity) {
		super.onAttach(activity);
		MainActivity main = ((MainActivity) activity);
		main.onSectionAttached(1);
	}

	@Override
	public void onDetach() {
		super.onDetach();
		mListener = null;
	}

	/**
	 * This interface must be implemented by activities that contain this
	 * fragment to allow an interaction in this fragment to be communicated to
	 * the activity and potentially other fragments contained in that activity.
	 * <p>
	 * See the Android Training lesson <a href=
	 * "http://developer.android.com/training/basics/fragments/communicating.html"
	 * >Communicating with Other Fragments</a> for more information.
	 */
	public interface OnFragmentInteractionListener {
		// TODO: Update argument type and name
		public void onFragmentInteraction(Uri uri);
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

		fillWebView(filename);
		txtFileName.setText(filename);
	}

	@Override
	public void onDirItemClick(String path) {
		/*setTitle(path);*/
		txtFileName.setText(path);
	}

	/*@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.controls_browser);
		String fileName = getIntent().getBundleExtra("bd")
				.getString("fileName");
		fileBrowser = (FileBrowser) findViewById(R.id.filebrowser);
		fileBrowser.setOnFileBrowserListener(this);
		fileBrowser.brower(fileName);
	}*/

	/*@Override
	public boolean onKeyDown(int keyCode, KeyEvent event) {
		if (keyCode == KeyEvent.KEYCODE_BACK && event.getRepeatCount() == 0) {
			if (!fileBrowser.getCanUp())
				finish();
			fileBrowser.Up();
			return true;
		} else
			return super.onKeyDown(keyCode, event);
	}*/
	private void LoadWebViewContent(WebView webView, String content) {

		webView.loadDataWithBaseURL("file:///android_asset/"
				+ getString(R.string.text_reader_html_assets_folder) + "/",
				content, "text/html",
				getString(R.string.text_reader_html_encoding), null);
	}

	public String readFle(String filelName) {
		Log.e("error", filelName);
		String content = "";
		try {
			InputStream in = getResources().getAssets().open(
					getString(R.string.text_reader_html_assets_folder) + "/"
							+ filelName);
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

			e.printStackTrace();
		}
		return outSteam.toByteArray();

	}

	public void fillWebView(String fileName) {

		if (fileName == null || fileName.equals(""))
			return;
		File f = new File(fileName);
		if (!f.exists() || !f.isFile())
			return;
		String htmlContent = readFle(getString(R.string.text_reader_html_filename));

		String showLinesTag = getString(R.string.text_reader_html_tag_showlines);
		String showLineValue = showLinesTag.replace("##", "");

		String wordWrapTag = getString(R.string.text_reader_html_tag_wordwrap);
		String wordWrapValue = wordWrapTag.replace("##", "");
		String contentTag = getString(R.string.text_reader_html_tag_content);

		htmlContent = htmlContent
				.replace(showLinesTag, showLines ? showLineValue : "")
				.replace(wordWrapTag, wordwrap ? wordWrapValue : "")
				.replace(contentTag, ReadTxtFile(fileName));
		String prefix = fileName.substring(fileName.lastIndexOf(".") + 1);
		if (prefix.toLowerCase().equals("html")
				|| prefix.toLowerCase().equals("htm")) {
			htmlContent = StringEscapeUtils.escapeHtml(htmlContent);
		}

		LoadWebViewContent(webView, htmlContent);
	}

	public static String ReadTxtFile(String strFilePath) {
		Log.e("error", strFilePath);
		String path = strFilePath;
		String content = ""; // 文件内容字符串
		// 打开文件
		File file = new File(path);
		// 如果path是传递过来的参数，可以做一个非目录的判断
		if (file.isDirectory()) {
			Log.d("TestFile", "The File doesn't not exist.");
		} else {
			try {
				InputStream instream = new FileInputStream(file);
				if (instream != null) {
					InputStreamReader inputreader = new InputStreamReader(
							instream);
					BufferedReader buffreader = new BufferedReader(inputreader);
					String line;
					// 分行读取
					while ((line = buffreader.readLine()) != null) {
						content += line + "\n";
					}
					instream.close();
				}
			} catch (java.io.FileNotFoundException e) {
				Log.d("TestFile", "The File doesn't not exist.");
			} catch (IOException e) {
				Log.d("TestFile", e.getMessage());
			}
		}
		return content;
	}
}
