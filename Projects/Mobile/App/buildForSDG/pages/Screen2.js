/*This is an example to Make Phone Call, Send SMS or Email Using React Native Communication*/
import React, {Component} from 'react';
//import React

import {StyleSheet, Text, View, TouchableOpacity} from 'react-native';
//import Basic React Components

import Communications from 'react-native-communications';
// either import the whole module and call as Communications.phonecall('0123456789', true)
// or can import single methods and call straight via the method name
// import { web, phonecall } from 'react-native-communications';
// e.g. onPress={() => { phonecall('0123456789', true) }}

export default class App extends Component{
  render() {
    return (
      <View style={styles.container}>
        <Text style={styles.headingStyle}>Ways to book your appointment quickly.</Text>
        
        {/*To make a phone call phonecall(phoneNumber, prompt) */}
        <TouchableOpacity 
          style = {styles.button}
          onPress={() => Communications.phonecall('+233542996109', true)}>
            <Text style={styles.text}>
              Make Phone Call To Book
            </Text>
        </TouchableOpacity>

        {/*To send the mail function(to, cc, bcc, subject, body)*/}
        <TouchableOpacity 
          style = {styles.button}
          onPress={() => Communications.email(['nyonouglokoffi8@gmail.com', 'buildforsdg@aboutreact.com'],null,null,'Hello Andela','Thanks for your support')}>
          {/*email(to, cc, bcc, subject, body)*/}
            <Text style={styles.text}>
              Send Us An Email
            </Text>
        </TouchableOpacity>

        {/*To send the text message function(phoneNumber = null, body = null)*/}
        <TouchableOpacity 
          style = {styles.button}
          onPress={() => Communications.text('+233542996109', 'Book your appointment from here')}>
            <Text style={styles.text}>
              Send a Text/iMessage
            </Text>
        </TouchableOpacity>

        {/*To open a web URL function(address = null)*/}
        <TouchableOpacity 
          style = {styles.button}
          onPress={() => Communications.web('http://onlineus.info/build/public/')}>
            <Text style={styles.text}>
              Open Web based app of this Hospital software.
            </Text>
        </TouchableOpacity>
      </View>
    );
  }
};

var styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    backgroundColor: 'rgb(253,253,253)',
    padding: 16,
  },
  headingStyle: {
    fontSize: 25,
    textAlign: 'center',
    padding: 30,
  },
  button: {
    justifyContent: 'center',
    width : 300,
    backgroundColor:"#307cae",
    marginTop : 20,
  },
  text: {
    fontSize: 18,
    textAlign : 'center',
    padding : 10,
    color : '#ffffff',
  },
});