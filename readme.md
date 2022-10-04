# Cman
Cman is a tool designed for developers to create software easily. Cman is a simple scripting language used to manage your c and c++ builds.



### Simple library and application build example
```
workspace("Sandbox") {
    configurations = {"Debug", "Release"}
    location = "build"
    
    //defines the platform name 
    define(os.name)
    
    //defines a library named "MyLib" that will automatially be mapped to a project with the path "MyLib"   
    project("MyLib") {
        kind = "Library"
        language = "C++"
        files = {"mylib.hpp", "src/*.c"}
    }
    
    //defines an executable named MyLib that will automatially be mapped to a project with the path "MyLib" 
    project("MyProgram") {
        kind = "Application"    
        language = "C++"
        files = {"include/dx11/*.h", "include/*.h", "main.cpp"}
        links("MyLib", "png", "zlib")
    }
}
```