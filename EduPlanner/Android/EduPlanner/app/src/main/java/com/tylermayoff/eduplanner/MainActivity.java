package com.tylermayoff.eduplanner;

import android.app.Fragment;
import android.app.FragmentTransaction;
import android.content.Intent;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_main);

        if (findViewById(R.id.mainFragView) != null) {

            if (savedInstanceState != null)
                return;

            AgendaView agendaView = new AgendaView();
            agendaView.setArguments(getIntent().getExtras());

            getSupportFragmentManager().beginTransaction().add(R.id.mainFragView, agendaView).commit();
        }
    }

    public void BtnAddClass_Click(View view) {
        AddClass addClass = new AddClass();

        Bundle args = new Bundle();
        args.putInt(addClass.ARG_POSITION, position);
        addClass.setArguments(args);

        FragmentTransaction fragmentTransaction = getFragmentManager().beginTransaction();
        fragmentTransaction.replace(R.id.mainFragView, addClass);
        fragmentTransaction.addToBackStack(null);

        fragmentTransaction.commit();
    }
}