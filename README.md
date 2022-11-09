# Trabalho para a disciplina de Teoria da Informação GB

### Para rodar é necessário:
- Instalar [Dotnet 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0 "Dotnet 6")
- Alguma IDE ou editor de texto ([VSCode](https://code.visualstudio.com/download "VSCode"), [Visual Studio](https://visualstudio.microsoft.com/pt-br/vs/community/ "Visual Studio"), [Rider](https://www.jetbrains.com/pt-br/rider/download/#section=windows "Rider"))
- Subir o projeto e seguir o menu, que aparecerá no console, de acordo com o que desejar

### Gerador de SubKeys
A implementação do gerador de subkey está implementada de acordo com os seguintes passos:

- O usuário informa uma chave de 4 bytes (32 bits)
- Realizado a operação de Left shift em cima desses 4 bytes fornecidos
- Os 4 bytes que foram fornecidos são invertidos
- Realizado uma operação Xor entre os bytes que foram invertidos e os bytes que sofreram o left shift
- Os bytes que sofreram xor são concatenados com os que estão invertidos formado strings de 8 Bytes onde byte inicial é alternado uma vez sim e outra não. Por exemplo: quando for par a string começará com o byte que foi invertido quando for impar começará com o que sofreu xor
- Os bytes que sofreram xor são concatenados com os que sofreram left shift formado strings de 8 bytes onde byte inicial é alternado uma vez sim e outra não. Por exemplo: quando for par a string começará com o byte que sofreu xor quando for impar começará com o que sofreu o left shift
- Ao final são geradas 8 subkeys de 2 bytes cada

#### Algoritimo de ciframento

// colocar aqui
