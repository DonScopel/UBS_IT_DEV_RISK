# UBS_IT_DEV_RISK

Com base no escopo do projeto, o console segue os passos abaixo:
- Lê arquivo com os portifolios;
- Ao ler, valida Data de Refêrencia, Número de Registros e os registros em si;
- Os registros são validados por Expressão Regular para verificar o padrão "Valor Setor Data";
- Os registros já são categorizados de acordo com o enunciado;
- Após todo o processo, os registros são salvos.

Tanto o arquivo de portifólio, quanto o de categorizados ficam na pasta do executável.
Há diversos tratamento de erros, todos exibidos em tela caso ocorram.
