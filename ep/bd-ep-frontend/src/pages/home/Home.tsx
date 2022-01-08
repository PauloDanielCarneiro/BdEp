import { useNavigation } from "@react-navigation/native";
import React from "react";
import { View, Text, StyleSheet } from 'react-native'
import { Button } from "react-native-paper";
import { homeProp } from "../../routes/params/AuthStackParams";

export default function Home() {
    
    const navigation = useNavigation<homeProp>()

    return (
        <View style={styles.container}>
            <View style={{ alignItems:"center", }}>
                <Text style={{fontSize: 34, fontWeight:"300"}}>Home</Text>
            </View>
            <View style={styles.buttonsContainer}>
                <View >
                    <Button mode='contained' 
                    labelStyle={styles.buttonLabel} 
                    style={styles.buttonStyle}
                    onPress={() => navigation.navigate("SignIn")}>
                        Login
                    </Button>
                </View>
                <View >
                    <Button mode="outlined" 
                    labelStyle={styles.buttonLabel} 
                    style={styles.buttonStyle}
                    onPress={() => navigation.navigate("SignUp")}>
                        Sign Up
                    </Button>
                </View>
            </View>

        </View>
    )
}

const styles = StyleSheet.create({
    container:{
        ...StyleSheet.absoluteFillObject,
        justifyContent:"center",
    },
    buttonsContainer:{
        flexDirection: "column",
        margin: 10,
        padding: 10,
    },
    buttonStyle:{
        margin: 10,
        padding: 10,
    },
    buttonLabel:{fontSize:16, fontWeight:"500"}
    

})