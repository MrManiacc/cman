lexer grammar CmanLexer;

//=============== Fragments ===========================
fragment LOWERCASE:  [a-z];
fragment UPPERCASE: [A-Z];
fragment DIGIT: [0-9];
fragment NEW_LINE: ('\n' | '\r');
fragment SPACE: ' ';


//================ Comments & white space =============
LINE_COMMENT: '//' (. | NEW_LINE)*? NEW_LINE -> skip;
MULTI_LINE_COMMENT: '/*' .*? '*/' -> skip;
WS : [ \t\r\n]+ -> skip ;

//=============== Labels ==============================
FUNCTION_LABEL: 'fn';
WORKSPACE_LABEL: 'workspace';
PROJECT_LABEL: 'project';
OVERRIDE_LABEL: 'veto';
VARIABLE_LABEL: 'let';
RETURN_LABEL: 'ret';
IF_LABEL: 'if';
ELSE_LABEL: 'else';
OBJECT_LABEL: 'obj';
DOUBLE_LABEL: 'double';
STRING_LABEL: 'string';
INT_LABEL: 'int';
BOOLEAN_LABEL: 'boolean';
FLOAT_LABEL: 'float';
LONG_LABEL: 'long';
CHAR_LABEL: 'char';
BYTE_LABEL: 'byte';
SHORT_LABEL: 'short';
VOID_LABEL: 'void';
NULL_LABEL: 'null';
FOR_LABEL: 'for';

//=============== Operators ===========================
PRINT: '>>';
OPEN_PAREN: '(';
CLOSE_PAREN: ')';
OPEN_SQUARE: '[';
CLOSE_SQUARE: ']';
OPEN_BRACKET: '{';
CLOSE_BRACKET: '}';
DOUBLE_COLON: '::';
COLON: ':';
SEMICOLON: ';';
COMMA: ',';
DOLLAR: '$';
TILDE: '~'; 
POUND: '#';
AT: '@';
EQUAL_TO: '==';
EQUAL: '=';
DOT: '.';
NOT: '!';
OR: '||';
AND: '&&';
NOT_EQUAL_TO: '!=';
LESS_THAN: '<';
GREATER_THAN: '>';
LESS_THAN_OR_EQUAL: '<=';
GREATER_THAN_OR_EQUAL: '>=';
PLUS: '+';
MINUS: '-';
MULTIPLY: '*';
DIVIDE: '/';
MODULUS: '%';

//=============== Captured values ===========================
BOOL: ('true' | 'false');
ID: (LOWERCASE | UPPERCASE) (LOWERCASE | UPPERCASE | '_' | DIGIT)*;
DECIMAL_NUMBER: '-'? DIGIT* ('.' DIGIT*);
STRING : '"'~('"')*'"' ;
WHOLE_NUMBER: '-'? DIGIT+;
NUMBER: DIGIT | WHOLE_NUMBER;