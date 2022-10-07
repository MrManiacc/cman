parser grammar CmanParser;

options{ tokenVocab = CmanLexer; }

program: atom* EOF;

//=========== Atom =================
atom: module | function | statement;

//=========== Modules =================
module: moduleImports? moduleSignature moduleBody;
moduleBody: OPEN_BRACKET atom* CLOSE_BRACKET;
moduleSignature: MODULE_LABEL moduleName;
moduleImports: USE_LABEL qualifiedName | USE_LABEL OPEN_BRACKET qualifiedName (COMMA qualifiedName)* CLOSE_BRACKET;

//=========== Functions =================
function: functionSignature statement;
functionSignature: FUNCTION_LABEL functionName OPEN_PAREN parameterList? CLOSE_PAREN returnType=explicitTypeSignature?;
parameterList: parameter | parameter (COMMA parameter)+;
parameter: name explicitTypeSignature;

//============== Variable/Field signature ============
variable: VARIABLE_LABEL name variableDeclartion;
variableDeclartion: explicitTypeSignature #ExplicitSignature 
                  | assignment #AssignmentSignature
                  | explicitTypeSignature assignment #ExplicitAssignmentSignature;
explicitTypeSignature: COLON type; // This is used for declaring exactly what your type is

//============== Annotations ============
//annotation: AT annotationName call;

//=========== Statements ===============
statement: block #BlockStmt
         | variable #VarDeclarationStmt
         | expression assignment #AssignmentStmt
         | expression binaryAssignment #AssignmentStmt
         | for #ForStmt
         | return #returnStmt
         | if #IfStmt
         | expression #ExpressionStmt;

if: IF_LABEL OPEN_PAREN expression CLOSE_PAREN trueStatement=statement (ELSE_LABEL falseStatement=statement)?;
return: RETURN_LABEL expression?;
block : OPEN_BRACKET statement* CLOSE_BRACKET;
assignment: EQUAL expression;
binaryAssignment: binaryOperator EQUAL expression; 

//=========== Expressions ===============
expression: owner=expression DOT functionName call #FunctionCallExpr 
          | functionName call #FunctionCallExpr
          | owner=expression DOT variableName #VarRefExpr
          | variableName #VarRefExpr
          | owner=expression OPEN_SQUARE target=expression CLOSE_SQUARE #ArrayVarRefExpr
          | valueList #ValueListExpr
          | spread #SpreadExpr
          | value #LiteralExpr
          | OPEN_PAREN left=expression binaryOperator right=expression CLOSE_PAREN #BinaryExpr
          | left=expression binaryOperator right=expression #BinaryExpr
          | OPEN_PAREN left=expression condtionalOperator right=expression CLOSE_PAREN #ConditionalExpr
          | left=expression condtionalOperator right=expression #ConditionalExpr
          | OPEN_PAREN operator=urnaryOperator expression CLOSE_PAREN #UnaryExpr
          | operator=urnaryOperator expression #UnaryExpr
          | OPEN_PAREN expression operator=urnaryOperator CLOSE_PAREN #UnaryExpr
          | expression operator=urnaryOperator #UnaryExpr
;

call: OPEN_PAREN expressionList? CLOSE_PAREN;
for: FOR_LABEL OPEN_PAREN variable COMMA condition=expression COMMA advancement=expression CLOSE_PAREN statement; 
valueList : OPEN_BRACKET expression (COMMA  expression)* CLOSE_BRACKET;
spread : DOT DOT DOT expression;
binaryOperator: MULTIPLY        #MultiplyOp
              | DIVIDE          #DivideOp
              | PLUS            #PlusOp
              | MINUS           #MinusOp
              | MODULUS         #ModulusOp
;
condtionalOperator: GREATER_THAN | LESS_THAN | EQUAL_TO | NOT_EQUAL_TO | GREATER_THAN_OR_EQUAL |  LESS_THAN_OR_EQUAL | AND | OR;
urnaryOperator: (PLUS PLUS) 
              | (MINUS MINUS)
              | NOT;

expressionList : expression (COMMA  expression)*;

//=============== Values ==============================
value: number #NumberValue
     | BOOL #BoolValue
     | CHAR #CharValue
     | STRING #StringValue
     | NULL_LABEL #NullValue; //TODO maybe remove null

//defines all of our raw captured number values
number: BYTE_VALUE #ByteValue 
      | SHORT_VALUE #ShortValue
      | INT_VALUE  #IntValue
      | LONG_VALUE #LongValue
      | DOUBLE_VALUE #LongValue
      | FLOAT_VALUE #FloatVla
;

//=============== Types ==============================
type : primitives #PrimativeType
  | qualifiedName #ClassType
  | (qualifiedName (OPEN_SQUARE CLOSE_SQUARE)+) #ClassArrayType;

primitives: OBJECT_LABEL    #ObjectType
          | BOOLEAN_LABEL   #BooleanType
          | STRING_LABEL    #StringType
          | CHAR_LABEL      #CharType
          | BYTE_LABEL      #ByteType
          | SHORT_LABEL     #ShortType
          | INT_LABEL       #IntType
          | LONG_LABEL      #LongType
          | FLOAT_LABEL     #FloatType
          | DOUBLE_LABEL    #DoubleType
          | VOID_LABEL      #VoidType
          | OBJECT_LABEL    (OPEN_SQUARE CLOSE_SQUARE)+   #ObjectArrayType
          | BOOLEAN_LABEL   (OPEN_SQUARE CLOSE_SQUARE)+   #BooleanArrayType
          | STRING_LABEL    (OPEN_SQUARE CLOSE_SQUARE)+   #StringArrayType
          | CHAR_LABEL      (OPEN_SQUARE CLOSE_SQUARE)+   #CharArrayType
          | BYTE_LABEL      (OPEN_SQUARE CLOSE_SQUARE)+   #ByteArrayType
          | SHORT_LABEL     (OPEN_SQUARE CLOSE_SQUARE)+   #ShortArrayType
          | INT_LABEL       (OPEN_SQUARE CLOSE_SQUARE)+   #IntArrayType
          | LONG_LABEL      (OPEN_SQUARE CLOSE_SQUARE)+   #LongArrayType
          | FLOAT_LABEL     (OPEN_SQUARE CLOSE_SQUARE)+   #FloatArrayType
          | DOUBLE_LABEL    (OPEN_SQUARE CLOSE_SQUARE)+   #DoubleArrayType
          | VOID_LABEL      (OPEN_SQUARE CLOSE_SQUARE)+   #VoidArrayType;

//=============== Names ==============================
variableName: ID;
functionName: ID;
moduleName: ID;
annotationName: ID;
qualifiedName : ID (DOT ID)* (DOT '*')?;
name : ID;
