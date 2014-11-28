// Created by plusminus on 17:37:14 - 24.11.2007
package org.anddev.android.filebrowser;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;


public class DirNameInputActivity extends Activity{
	
	protected final int SUCCESS_RETURN_CODE = 1;
    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle icicle) {
        super.onCreate(icicle);
     // Apply our subactivity.xml-Layout
        setContentView(R.layout.filenameinput);
        
        // Find the button defined in the subactivity.xml
        Button cmd_return = (Button)findViewById(R.id.subactivity_cmd_return);
        /* Add an OnClickListener to it, that will
         * read out the text in the EditBox set it 
         * as retrn-data and close this activity */
        cmd_return.setOnClickListener(new OnClickListener(){
			// @Override
			public void onClick(View arg0) {
				// Find the edittext defined in the subactivity.xml
				EditText edit_returvalue = (EditText)findViewById(
						R.id.subactivity_edit_returnvalue);
				Intent i=new Intent(); 
				DirNameInputActivity.this.setResult(SUCCESS_RETURN_CODE, 
					i);//	edit_returvalue.getText().toString()
				// Close this Activity
				DirNameInputActivity.this.finish();
			}
        });       
    }
}

