//This is an example code for Navigation Drawer with Custom Side bar//
import React, { Component } from 'react';
import {   ActivityIndicator } from 'react-native';
//import react in our code.
import { StyleSheet, View, Text } from 'react-native';
// import all basic components

import { WebView } from 'react-native-webview';

//loading indcator
function LoadingIndicatorView() {
  return <ActivityIndicator color='#009b88' size='large' />
}

export default class Screen1 extends Component {
  //Screen1 Component
  render() {

    return <WebView 
    originWhitelist={['*']}
    source={{ uri: 'http://onlineus.info/build/public/' }} 

    renderLoading={this.LoadingIndicatorView}
        startInLoadingState={true}

    />;
  }
}

