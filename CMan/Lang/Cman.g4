grammar Cman;

script : ows (workspace ows)* EOF ;

//=============== Workspace  ========================
workspace: WORKSPACE_LABEL ows OPEN_PAREN expression ows CLOSE_PAREN ows block;

//=============== Project  ========================
project: PROJECT_LABEL ows OPEN_PAREN expression ows CLOSE_PAREN ows block;

//=============== Fields  ========================
field: VARIABLE_LABLE rws fieldName ows typeSignature;

//============== Variable/Field signature ============
typeSignature: explicitTypeSignature | implicitTypeSignature | explicitTypeSignature ows implicitTypeSignature;
explicitTypeSignature: COLON ows type; // This is used for declaring exactly what your type is
implicitTypeSignature: EQUAL ows expression; // The expression's evaulated type will be used for the variable type. This means we don't need to have a signature for declarations

//=============== Functions  ========================
function: functionDeclaration functionBody;
functionDeclaration : FUNCTION_LABEL rws functionName ows (OPEN_PAREN ows parametersList? ows CLOSE_PAREN ows)? (COLON ows type ows)?;
functionBody: returnStatement | assignment | block;
parametersList:  parameter ows (COMMA ows parameter)*;
parameter : ID ows COLON ows type (ows EQUAL ows defaultValue=expression)?;

//=============== Statement ==========================
statement: block
         | variableDeclaration
         | assignment
         | printStatement
         | forStatement
         | returnStatement
         | ifStatement
         | project
         | expression;

spreadExpression: DOT DOT DOT ID?;
returnStatement: RETURN_LABEL ows expression #ReturnWithValue | RETURN_LABEL #ReturnVoid;
block : OPEN_BRACKET ows (statement ows)* CLOSE_BRACKET ;
printStatement: PRINT ows expression;
assignment: name ows EQUAL ows expression;
variableDeclaration: VARIABLE_LABLE rws name ows typeSignature;
ifStatement: IF_LABLE ows (OPEN_PAREN ows)? expression ows (CLOSE_PAREN ows)? trueStatement=statement ows (ELSE_LABLE ows falseStatement=statement ows)?;
forStatement: FOR_LABEL ows (OPEN_PAREN  ows)? forConditions ows (CLOSE_PAREN)? ows statement;
forConditions: iterator=variableReference rws IN_LABEL rws startExpr=expression rws TO_LABEL rws endExpr=expression;

//=============== Arguments ==========================
argumentList : argument? ( ows COMMA ows a=argument)* #UnnamedArgumentsList | namedArgument? (ows COMMA ows namedArgument)* #NamedArgumentsList ;
argument : expression;
namedArgument : name ows '->' ows expression;


//=============== Expression =========================
expression: owner=expression DOT functionName ows call #FuncCallExpr
          | functionName ows call #FuncCallExpr
          | variableReference #VarRefExpr
          | owner=expression DOT variableReference #VarRefExpr
          | variableReference ows call #InvokerExpr
          | superCall=SUPER_LABLE ows (DOT ows functionName ows)? call #SuperCallExpr
          | newCall = DOLLAR ows className ows call #CtorCallExpr
          | value #ValueExpr
          | listLiteral #ListExpr
          | spreadExpression #SpreadExpr
          | OPEN_PAREN ows expression ows MULTIPLY ows expression ows CLOSE_PAREN #MultiplyExpr
          | expression ows MULTIPLY ows expression #MultiplyExpr
          | OPEN_PAREN ows expression ows DIVIDE ows expression ows CLOSE_PAREN #DivideExpr
          | expression ows DIVIDE ows expression #DivideExpr
          | OPEN_PAREN ows expression ows PLUS ows expression ows CLOSE_PAREN #AddExpr
          | expression ows PLUS ows expression #AddExpr
          | OPEN_PAREN ows expression ows MINUS ows expression ows CLOSE_PAREN #SubtractExpr
          | expression ows MINUS ows expression #SubtractExpr
          | expression ows cmp=GREATER_THAN ows expression #ConditionalExpression
          | expression ows cmp=LESS_THAN ows expression #ConditionalExpression
          | expression ows cmp=EQUAL_TO ows expression #ConditionalExpression
          | expression ows cmp=NOT_EQUAL_TO ows expression #ConditionalExpression
          | expression ows cmp=GREATER_THAN_OR_EQUAL ows expression #ConditionalExpression
          | expression ows cmp=LESS_THAN_OR_EQUAL ows expression #ConditionalExpression;

call: (OPEN_PAREN ows argumentList ows CLOSE_PAREN ows)? unit
     | (OPEN_PAREN ows argumentList ows CLOSE_PAREN);

variableReference: ID;

value: WHOLE_NUMBER #IntegerValue
     | DECIMAL_NUMBER #DecimalValue
     | BOOL #BoolValue
     | STRING #StringValue
     | NULL_LABEL #NullValue; //TODO maybe remove null

//=============== List ==========================
listLiteral: OPEN_BRACKET ows ((expression | expression ows COMMA) ows)* CLOSE_BRACKET;

//=============== Lambda/Unit ========================
unit: unitDeclaration  #BlockUnit
    | unitReference #ReferenceUnit;
unitReference: qualifiedName? DOUBLE_COLON name; //System.out@println or should we do System.out::println
unitDeclaration: (OPEN_BRACKET (ows unitHeader)? unitBody CLOSE_BRACKET);

unitHeader: name ows (COLON ows type ows)? (COMMA ows name ows (COLON ows type ows)?)* PIPE ows;
unitBody: (ows statement ows)*;


//=============== Types ==============================
type : unitType | primitiveType | classType ;
unitType: OPEN_PAREN ows (type ows)?
        (COMMA ows type ows)* CLOSE_PAREN
        (ows RETURN_LABEL ows type)?;
classType : qualifiedName (OPEN_SQUARE CLOSE_SQUARE)* ;
primitiveType: BOOLEAN_LABEL (OPEN_SQUARE CLOSE_SQUARE)*
             | STRING_LABEL (OPEN_SQUARE CLOSE_SQUARE)*
             | CHAR_LABEL (OPEN_SQUARE CLOSE_SQUARE)*
             | BYTE_LABEL (OPEN_SQUARE CLOSE_SQUARE)*
             | SHORT_LABEL (OPEN_SQUARE CLOSE_SQUARE)*
             | INT_LABEL (OPEN_SQUARE CLOSE_SQUARE)*
             | LONG_LABEL (OPEN_SQUARE CLOSE_SQUARE)*
             | FLOAT_LABEL (OPEN_SQUARE CLOSE_SQUARE)*
             | DOUBLE_LABEL (OPEN_SQUARE CLOSE_SQUARE)*
             | VOID_LABEL (OPEN_SQUARE CLOSE_SQUARE)*;


//=============== Names ==============================
className: ID | ID (DOT ID)+;
functionName : modifiers ID ;
qualifiedName : ID (DOT ID)* (DOT MULTIPLY)?;
name : ID;
fieldName: modifiers name ;

//================ Helpers & Comments =============
rws: ((comment*) WHITE_SPACE (comment*))+;
ows: (comment | WHITE_SPACE)*;
comment: (LINE_COMMENT | MULTI_LINE_COMMENT);
modifiers: (privateModifer | staticModifer | finalModifier | protectedModifier)*;
privateModifer: TILDE;
staticModifer: AT;
finalModifier: POUND;
protectedModifier: MULTIPLY;


//=============== Fragments ===========================
fragment LOWERCASE:  [a-z];
fragment UPPERCASE: [A-Z];
fragment DIGIT: [0-9];
fragment NEW_LINE: ('\n' | '\r');
fragment SPACE: ' ';



//================ Modifiers ==========================


//================ Comments & white space =============
LINE_COMMENT: '//' (. | NEW_LINE)*? NEW_LINE;
MULTI_LINE_COMMENT: '/*' .*? '*/';
WHITE_SPACE: (' ' | '\r' | '\t' | '\n');
//=============== Captured Values =====================
BOOL : 'true' | 'false';



//=============== Labels ==============================
CLASS_LABEL: 'class';
WORKSPACE_LABEL: 'workspace';
PROJECT_LABEL: 'project';
IF_LABLE: 'if';
ELSE_LABLE: 'else';
SUPER_LABLE: 'super';
AS_LABEL: 'as';
VARIABLE_LABLE: 'var';
RETURN_LABEL: '->';
FUNCTION_LABEL: 'fn';
IMPORT_LABEL: 'use';
NULL_LABEL: 'nil';
DOUBLE_LABEL: 'double';
STRING_LABEL: 'String';
INT_LABEL: 'int';
BOOLEAN_LABEL: 'boolean';
FLOAT_LABEL: 'float';
LONG_LABEL: 'long';
CHAR_LABEL: 'char';
BYTE_LABEL: 'byte';
SHORT_LABEL: 'short';
VOID_LABEL: 'void';
FOR_LABEL: 'for';
IN_LABEL: 'in';
TO_LABEL: 'to';
ID: (LOWERCASE | UPPERCASE) (LOWERCASE | UPPERCASE | '_' | DIGIT)*;

//=============== Operators ===========================
PRINT: '>>';
OPEN_PAREN: '(';
CLOSE_PAREN: ')';
OPEN_SQUARE: '[';
CLOSE_SQUARE: ']';
OPEN_BRACKET: '{';
CLOSE_BRACKET: '}';
PIPE: '|';
DOUBLE_COLON: '::';
COLON: ':';
SEMICOLON: ';';
COMMA: ',';
DOLLAR: '$';
TILDE: '~'; //final
POUND: '#';
AT: '@'; //Static
EQUAL_TO: '==';
EQUAL: '=';
DOT: '.';
NOT: '!';
NOT_EQUAL_TO: '!=';
LESS_THAN_OR_EQUAL: '<=';
GREATER_THAN_OR_EQUAL: '>=';
LESS_THAN: '<';
GREATER_THAN: '>';
PLUS: '+';
MINUS: '-';
MULTIPLY: '*';
DIVIDE: '/';
MODULUS: '%';

DECIMAL_NUMBER: '-'? DIGIT* ('.' DIGIT*);
STRING : '"'~('"')*'"' ;
WHOLE_NUMBER: '-'? DIGIT+;
NUMBER: DIGIT | WHOLE_NUMBER;