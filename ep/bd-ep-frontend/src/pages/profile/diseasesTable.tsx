/* import React from "react";
import { View, Text, StyleSheet, Platform } from 'react-native'
import { Button, Checkbox, DataTable, List, Modal, Portal, } from "react-native-paper";
import { useAuth } from "../../contexts/auth";


interface diseaseTable{
    id: string;
    name: string;
    startDate: Date;
    endDate: Date;
}

export default function DiseasesTable() {
    
    const { token } = useAuth()
    
    const [page, setPage] = React.useState<number>(0);
  
    
    

    return (
        <>
            <View style={styles.dataTableContainer}>
                <Text style={{fontSize:30, fontWeight:'700'}}>Diseases</Text>
                <DataTable>
                    <DataTable.Header>
                        <DataTable.Title>Dessert</DataTable.Title>
                        <DataTable.Title numeric>Calories</DataTable.Title>
                        <DataTable.Title numeric>Fat</DataTable.Title>
                    </DataTable.Header>

                    <DataTable.Row>
                        <DataTable.Cell>Ice cream sandwich</DataTable.Cell>
                        <DataTable.Cell numeric>237</DataTable.Cell>
                        <DataTable.Cell numeric>8.0</DataTable.Cell>
                    </DataTable.Row>
                    <DataTable.Row>
                        <DataTable.Cell>Ice cream sandwich</DataTable.Cell>
                        <DataTable.Cell numeric>237</DataTable.Cell>
                        <DataTable.Cell numeric>8.0</DataTable.Cell>
                    </DataTable.Row><DataTable.Row>
                        <DataTable.Cell>Ice cream sandwich</DataTable.Cell>
                        <DataTable.Cell numeric>237</DataTable.Cell>
                        <DataTable.Cell numeric>8.0</DataTable.Cell>
                    </DataTable.Row>
                    <DataTable.Row>
                        <DataTable.Cell>Ice cream sandwich</DataTable.Cell>
                        <DataTable.Cell numeric>237</DataTable.Cell>
                        <DataTable.Cell numeric>8.0</DataTable.Cell>
                    </DataTable.Row>
                    <DataTable.Row>
                        <DataTable.Cell>Ice cream sandwich</DataTable.Cell>
                        <DataTable.Cell numeric>237</DataTable.Cell>
                        <DataTable.Cell numeric>8.0</DataTable.Cell>
                    </DataTable.Row>
                   
                   

                    <DataTable.Pagination
                        page={page}
                        numberOfPages={10}
                        onPageChange={(page) => {  }}
                        label="1-2 of 6"
                    />
                    </DataTable>
            </View>
        </>
    )
}

const styles = StyleSheet.create({
    container:{
        ...StyleSheet.absoluteFillObject,
        justifyContent:"center",
    },
    
    
    InputContainer:{
        position:'relative',
        height: 40,
        margin:5,
        backgroundColor:"red",
        flexDirection:"row",
        zIndex:-1
        
    },
    dataTableContainer:{
        marginHorizontal: 10,
        borderColor:"black",
        borderWidth:1

    },
    


}) */