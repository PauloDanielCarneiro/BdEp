/* import { useNavigation } from '@react-navigation/native';
import React, { useContext } from 'react';
import { View, Text, StyleSheet } from 'react-native'
import { Button, Modal, Portal, TextInput } from 'react-native-paper';

import AuthContext from '../contexts/auth';
import { signInProp } from '../routes/params/AuthStackParams';

const SignIn: React.FC = () =>  {
  const {signed, signIn} =  useContext(AuthContext);
  const navigation = useNavigation<signInProp>();

    async function handleSign() {
      //criar function para verificar email e senha valido
      
      signIn();
    }
    function handleSignUp(){
      navigation.navigate("SignUp");
    }
    
    const [email, setEmail] = React.useState("");
    const [pw, setPw] = React.useState("");
    const [isVisible, setIsVisible] = React.useState(true);

    return (
    <>
      <Portal>
      <Modal visible={isVisible} 
      contentContainerStyle={styles.modalContainer} 
      dismissable={false}>

        <View style={styles.container}>
          <Text style={{ fontWeight:"200", fontSize:24, alignSelf:"center", marginTop:20}}>Fazer login</Text>
            <View style={ styles.signInContainer }>
              <View style={ styles.inputContainer }>
                  <TextInput 
                  mode='outlined' label={'Email'} 
                  onChangeText={(text) => {setEmail(text)}}/>
                  </View>
              <View style={ styles.inputContainer }>
                  <TextInput 
                  mode='outlined' label={'Senha'} 
                  onChangeText={(text) => {setPw(text)}}/>
              </View>
            </View>
            <View style={styles.buttonContainer}>
                <Button style={styles.buttonStyle} color='green'
                mode="contained" onPress={handleSign}>
                    Sign In
                </Button>
                <Button style={styles.buttonStyle} color='red'
                mode="contained" onPress={() => { handleSignUp }}>
                    Sign Up
                </Button>
            </View>
        </View>
    </Modal>
    </Portal>
    </>
    )
}

const styles = StyleSheet.create({
  modalContainer: {
    borderRadius: 18, 
    backgroundColor:"white",
    justifyContent: "center",
    marginHorizontal: 16,
    
  },
  container: {
    
  },
  signInContainer: {
    margin: 10,
    padding: 10
  },
  inputContainer: {
  },
  buttonContainer: {
    flexDirection: "row", 
    justifyContent:"center",
    marginVertical: 10
  },
  buttonStyle: {
    margin: 5,
    padding: 5
  },
    
  });
  
  export default SignIn */