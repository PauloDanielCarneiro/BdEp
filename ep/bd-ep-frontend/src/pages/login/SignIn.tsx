import React, { useContext } from "react";
import { useNavigation } from "@react-navigation/native";
import { View, Text, StyleSheet } from 'react-native'
import { Button, TextInput } from "react-native-paper";
import { useAuth } from "../../contexts/auth";
import { signInProp } from "../../routes/params/AuthStackParams";

export default function SignIn() {

    //TODO:Validacao dos TextInput
    
    const navigation = useNavigation<signInProp>()
    const { signIn } =  useAuth();
    const [cpf, setCpf] = React.useState("")
    const [pw, setPw] = React.useState("")
    async function handleSign() {

        signIn(cpf, pw);
    }

    return (
        <View style={styles.container}>
          <Text style={{ fontWeight:"500", fontSize:24, alignSelf:"center", marginTop:20}}>Fazer login</Text>
            <View style={ styles.signInContainer }>
              <View style={{marginVertical: 10}}>
                  <TextInput 
                  mode='outlined' label={'CPF'} 
                  onChangeText={(text) => {setCpf(text)}}/>
                  </View>
              <View style={{marginVertical: 10}}>
                  <TextInput 
                  mode='outlined' label={'Senha'} secureTextEntry={true}
                  onChangeText={(text) => {setPw(text)}}/>
              </View>
            </View>
            <View style={styles.buttonContainer}>
                <Button style={styles.buttonStyle} color="red" 
                mode="outlined" onPress={() => navigation.goBack()}>
                    Voltar
                </Button>
                <Button style={styles.buttonStyle} 
                mode="contained" onPress={handleSign}> 
                    Sign In
                </Button>
            </View>
        </View>
    )
}

const styles = StyleSheet.create({
    container:{
        ...StyleSheet.absoluteFillObject,
        justifyContent:"center",
    },
    signInContainer:{
        flexDirection: "column",
        margin: 10,
        padding: 10,
    },
    buttonContainer:{
        flexDirection: "row",
        justifyContent:"center",
    },
    buttonStyle:{
        margin: 10,
        padding: 10,
    },

})