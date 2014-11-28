package com.justin.reader.fragment;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;

import com.justin.reader.MainActivity;
import com.justin.reader.R;

import android.app.Activity;
import android.net.Uri;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebView;

/**
 * A simple {@link Fragment} subclass. Activities that contain this fragment
 * must implement the {@link TextReaderFragment.OnFragmentInteractionListener}
 * interface to handle interaction events. Use the
 * {@link TextReaderFragment#newInstance} factory method to create an instance
 * of this fragment.
 * 
 */
public class TextReaderFragment extends Fragment {
	// TODO: Rename parameter arguments, choose names that match
	// the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
	private static final String ARG_PARAM1 = "param1";
	private static final String ARG_PARAM2 = "param2";
	private WebView webView;
	// TODO: Rename and change types of parameters
	private String mParam1;
	private String mParam2;
	private boolean showLines = true;
	private boolean wordwrap = true;
	private OnFragmentInteractionListener mListener;

	/**
	 * Use this factory method to create a new instance of this fragment using
	 * the provided parameters.
	 * 
	 * @param param1
	 *            Parameter 1.
	 * @param param2
	 *            Parameter 2.
	 * @return A new instance of fragment TextReaderFragment.
	 */
	// TODO: Rename and change types and number of parameters
	public static TextReaderFragment newInstance(String param1, String param2) {
		TextReaderFragment fragment = new TextReaderFragment();
		Bundle args = new Bundle();
		args.putString(ARG_PARAM1, param1);
		args.putString(ARG_PARAM2, param2);
		fragment.setArguments(args);
		return fragment;
	}

	public TextReaderFragment() {
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
		View rootView = inflater.inflate(R.layout.fragment_text_reader,
				container, false);

		webView = (WebView) rootView.findViewById(R.id.webView);
		webView.getSettings().setJavaScriptEnabled(true);

		webView.requestFocus();
		// webView.loadUrl("file:///android_asset/index1.html");// ¼ÓÔØhtmlÎÄ¼þ

		String htmlContent = readFle(getString(R.string.text_reader_html_filename));

		String showLinesTag = getString(R.string.text_reader_html_tag_showlines);
		String showLineValue = showLinesTag.replace("##", "");

		String wordWrapTag = getString(R.string.text_reader_html_tag_wordwrap);
		String wordWrapValue = wordWrapTag.replace("##", "");
		String contentTag = getString(R.string.text_reader_html_tag_content);

		htmlContent = htmlContent
				.replace(showLinesTag, showLines ? showLineValue : "")
				.replace(wordWrapTag, wordwrap ? wordWrapValue : "")
				.replace(
						contentTag,
						readFle(getString(R.string.text_reader_html_testfilename)));
		LoadWebViewContent(webView, htmlContent);

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
		/*try {
			mListener = (OnFragmentInteractionListener) activity;
		} catch (ClassCastException e) {
			throw new ClassCastException(activity.toString()
					+ " must implement OnFragmentInteractionListener");
		}*/

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

	private void LoadWebViewContent(WebView webView, String content) {
		webView.loadDataWithBaseURL("file:///android_asset/"
				+ getString(R.string.text_reader_html_assets_folder) + "/",
				content, "text/html",
				getString(R.string.text_reader_html_encoding), null);
	}

	public String readFle(String filelName) {
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
}
