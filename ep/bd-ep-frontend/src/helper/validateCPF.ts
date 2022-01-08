export default function validateCPF(strCPF:string) {
    var Soma;
    var Resto;
    Soma = 0;
    /* if (strCPF.length < 11) return 'CPF deve ter 11 dígitos'
    if (strCPF == "00000000000") return 'CPF Inválido';

    for ( var i=1; i<=9; i++ ) Soma = Soma + parseInt(strCPF.substring(i-1, i)) * (11 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11))  Resto = 0;
    if (Resto != parseInt(strCPF.substring(9, 10)) ) 'CPF Inválido';

    Soma = 0;
    for ( var i = 1; i <= 10; i++ ) Soma = Soma + parseInt(strCPF.substring(i-1, i)) * (12 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11))  Resto = 0;
    if (Resto != parseInt(strCPF.substring(10, 11) ) ) 'CPF Inválido';
    return ''; */
    return ""
}
// testar var strCPF = "12345678909";