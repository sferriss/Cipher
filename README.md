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

### Algoritimo de ciframento
A implementação do cifrador de bloco segue os seguintes passos:

- adiciona padding no arquivo para ele ter um tamanho multiplo de 6

- separa um bloco de 6 bytes do array de byte de entrada gerado pelo arquivo e padding

- esse bloco é dividido em dois blocos de 3 bytes sendo eles lBlock e rBlock

- o lBlock e rBlock passam por um round onde é feito um xor do lBlock com o resultado da 
passagem do rBlock pela função F

- depois é começado um novo round invertendo a entrada dos blocos, onde o lBlock depois do xor é passado pro rBlock e 
o rBlock passado paro o lBlock;

- no total são 8 rounds, um para cada sub-chave;

- a função f faz um xor do rBlock e da sub-chave do round;

- o resultado desse fluxo(6 bytes ciffrados) é salvo em um array de saída junto com um cabeçalho de 1 byte com a quantidade de padding inserida no arquivo original;

- esse fluxo ocorre o numero de vezes necessario para cifrar todo o arquivo, dependendo do seu tamanho;

- o mesmo ocoore no descriframento, porém usando as sub-chaves na ordem inversa em cada round, além da troca entre o lBlock e rBlock que é invertida também;

### Algumas considerações:
- Os arquivo codificados e decodificados ficam salvos na pasta ReturnedFiles
- A parte do CBC não foi implementada
