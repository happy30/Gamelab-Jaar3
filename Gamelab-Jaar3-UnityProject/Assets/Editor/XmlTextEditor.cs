using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;

using XmlEditor;

public class XmlTextEditor : EditorWindow {

    const string RESOURCES_DIR = "Assets/XML_Editor/Resources/";
    const string DATA_DIR = "Assets/XML_Editor/Editor/";
    const string DATA_FILE = "XMLEditorData";

    GUIStyle miniButtonStyle;
    GUIStyle areaStyle;
    GUIStyle buttonStyle;
    GUIStyle bigButtonStyle;
    GUIStyle elementSelectorButtonStyle;

    System.Reflection.FieldInfo[] fields;

    bool addList = false;
    bool optionList = false;
    DataString newData;

    int lastSelectedData = 0;
    int selectedData = 0;

    int selectedElement = 0;

    List<DataString> datas;
    List<IList> objectsLists;

    [MenuItem("Window/XML Editor")]
    static void DisplayWindow() {
        XmlTextEditor mpwind = GetWindow<XmlTextEditor>(false, "XML Editor");
        mpwind.minSize = new Vector2(500, 300);

        mpwind.selectedElement = 0;

    }

    void InitData() {
        
        datas = XmlReader.LoadXMLFrom<DataString>(DATA_DIR + DATA_FILE);
        //datas = XmlReader.LoadXML<DataString>(DATA_FILE);

        readData();

    }


    void createButtonStyle() {
        miniButtonStyle = new GUIStyle(EditorStyles.toolbarButton);
        miniButtonStyle.fixedWidth = 20;
        miniButtonStyle.fixedHeight = 20;
        
        areaStyle = new GUIStyle("box");

        elementSelectorButtonStyle = new GUIStyle(EditorStyles.toolbarButton);
        elementSelectorButtonStyle.margin.right = 2;
        //elementSelectorButtonStyle.stretchWidth = true;
       // bigButtonStyle.fixedHeight = 50;
        //bigButtonStyle.fontSize = 20;

        buttonStyle = new GUIStyle(EditorStyles.toolbarButton);
        buttonStyle.fixedHeight = 50;
        buttonStyle.fixedWidth = 50;
        buttonStyle.margin.right = 140;

        bigButtonStyle = new GUIStyle(EditorStyles.toolbarButton);
        bigButtonStyle.fixedHeight = 50;
        bigButtonStyle.fontSize = 20;
    }
    

    void OnGUI() {
        
        if(objectsLists == null) {
            InitData();
        }
        
        if (addList) {
            addListWindow();
            return;
        }else if (optionList) {
            optionListWindow();
            return;
        }
        

        createButtonStyle();

        drawListSelector();

        if (objectsLists.Count > 0) {

            changeSelectedData();

            drawElementSelector();
            drawElementAddRemove();
            drawSaveButton();

            GUI.color = Color.white;
            drawElements();
        }


    }

    void changeSelectedData() {

        if (objectsLists.Count == 0) return;
        
        if(selectedData != lastSelectedData || selectedData == -1) {

            if (selectedData < 0) selectedData = 0;

            lastSelectedData = selectedData;
            selectedElement = 0;

            fields = objectsLists[selectedData].GetType().GetGenericArguments()[0].GetFields();

        }

    }

    void drawListSelector() {

        GUIStyle areaStyle = new GUIStyle("box");
        GUIStyle buttonStyle = new GUIStyle(EditorStyles.toolbarButton);
        buttonStyle.fixedHeight = 25;
        buttonStyle.fixedWidth = 25;


        GUIStyle poputStyle = new GUIStyle(EditorStyles.popup);
        poputStyle.fixedHeight = 25;

        GUI.color = Color.gray;
        GUILayout.BeginArea(new Rect(position.width - 250, 0, 250, 55), areaStyle);

        GUILayout.BeginHorizontal();

        GUI.color = new Color(.8f, .8f, .8f);
        if (objectsLists != null) {

            string[] names = new string[datas.Count];
            for (int i = 0; i < datas.Count; i++) {
                names[i] = datas[i].displayName;
            }

            selectedData = EditorGUILayout.Popup(lastSelectedData, names, poputStyle);

            GUILayout.BeginVertical();

            if (GUILayout.Button("+", buttonStyle)) {
                addList = true;
                newData = new DataString();
            }

            if (datas.Count > 0) {

                if (GUILayout.Button("C", buttonStyle)) {
                    optionList = true;
                }
            }


            GUILayout.EndVertical();

        }

        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
    
    void addListWindow() {
        BeginWindows();
        GUILayout.Window(0, new Rect(20, 20, position.width - 40, position.height - 40), addListWindowContent, "Add XML");
        EndWindows();
    }

    void addListWindowContent(int windowID) {

        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Display Name", GUILayout.Width(120));
        newData.displayName = GUILayout.TextField(newData.displayName);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Class Name", GUILayout.Width(120));
        newData.className = GUILayout.TextField(newData.className);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Variable ID name", GUILayout.Width(120));
        newData.variableID = GUILayout.TextField(newData.variableID);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("XML File Name", GUILayout.Width(120));
        newData.directory = GUILayout.TextField(newData.directory);
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Close")) {
            addList = false;
        }
        if (GUILayout.Button("Save")) {

            datas.Add(newData);
            readData();


            addList = false;
        }

        GUILayout.EndHorizontal();
    }

    void optionListWindow() {

        BeginWindows();
        GUILayout.Window(0, new Rect(20, 20, position.width - 40, position.height - 40), optionListWindowContent, "XML Options");
        EndWindows();
    }

    Vector2 scrollOptions;
    void optionListWindowContent(int windowID) {

        scrollOptions = GUILayout.BeginScrollView(scrollOptions);
        GUILayout.BeginVertical();

        GUILayout.Label("XML directory: " + RESOURCES_DIR + datas[selectedData].directory + ".xml", EditorStyles.boldLabel);
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Remove XML from editor")) {
            objectsLists.RemoveAt(selectedData);

            datas.Remove(datas[selectedData]);
            selectedData = -1;

            changeSelectedData();
            /*datas.Add(newData);
            readData();*/

            optionList = false;
        }
        if (GUILayout.Button("Close")) {
            optionList = false;
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        GUILayout.EndScrollView();
    }

    Vector2 selectorScrollPosition;

    void drawElementSelector() {


        GUI.color = Color.gray;
        GUILayout.BeginArea(new Rect(position.width - 250, 55, 250, position.height - 165), areaStyle);

        GUI.color = new Color(.8f, .8f, .8f);
        selectorScrollPosition = GUILayout.BeginScrollView(selectorScrollPosition);
        GUILayout.BeginVertical();

        if (objectsLists != null) {
            for (int i = 0; i < objectsLists[selectedData].Count; i++) {
                //(string)typeof(string).GetField("name").GetValue(objectsLists[selectedData][i])

                object name = objectsLists[selectedData].GetType().GetGenericArguments()[0].GetField(datas[selectedData].variableID).GetValue(objectsLists[selectedData][i]);
                if (name == null) {
                    name = "NEW";
                    objectsLists[selectedData].GetType().GetGenericArguments()[0].GetField(datas[selectedData].variableID).SetValue(objectsLists[selectedData][i], name);
                }
                //name = objectsLists[selectedData].GetType().GetGenericArguments()[0].GetField(datas[selectedData].variableID).GetValue(objectsLists[selectedData][i]).ToString();

                if ( GUILayout.Toggle(selectedElement == i, name.ToString(), elementSelectorButtonStyle)) {
                    selectedElement = i;
                }
            }
        }


        GUILayout.EndVertical();
        GUILayout.EndScrollView();

        GUILayout.EndArea();

    }

    void drawElementAddRemove() {

        GUI.color = Color.gray;
        GUILayout.BeginArea(new Rect(position.width - 250, position.height - 110, 250, 55), areaStyle);

        GUILayout.BeginHorizontal();

        GUI.color = new Color(.8f, .8f, .8f);
        if(GUILayout.Button("-", buttonStyle)) {
            removeElement();
        }

        if (GUILayout.Button("+", buttonStyle)) {
            addElement();
        }

        GUILayout.EndHorizontal();

        GUILayout.EndArea();


    }

    void drawSaveButton() {
        GUI.color = Color.gray;
        GUILayout.BeginArea(new Rect(position.width - 250, position.height - 55, 250, 55), areaStyle);

        GUI.color = new Color(.8f, .8f, .8f);
        if (GUILayout.Button("SAVE", bigButtonStyle)) {
            saveData();
        }

        GUILayout.EndArea();
    }

    void addElement() {
        
        objectsLists[selectedData].Add(System.Activator.CreateInstance(objectsLists[selectedData].GetType().GetGenericArguments()[0]));
        selectedElement = objectsLists[selectedData].Count - 1;

    }

    void removeElement() {
        objectsLists[selectedData].RemoveAt(selectedElement);
        if (selectedElement > 0) selectedElement--;

        if(objectsLists[selectedData].Count == 0) {
            addElement();
        }
    }


    Vector2 elementScrollPosition;
    void drawElements() {
        if (objectsLists != null) {
            //for (int i = 0; i < rooms.Count; i++) {
            GUILayout.BeginArea(new Rect(5, 5, position.width - 255, position.height - 5));
            elementScrollPosition = GUILayout.BeginScrollView(elementScrollPosition);

            createElement(objectsLists[selectedData][selectedElement], objectsLists[selectedData], selectedElement);

            GUILayout.EndVertical();
            GUILayout.EndArea();
            //}
            
        }
    }

    
    void saveData() {
        if (objectsLists == null) return;
        if (objectsLists.Count == 0) return;

        if (objectsLists[selectedData].Count == 0) return;


        //XmlReader.SaveXml(DATA_FILE, datas);
        XmlReader.SaveXmlTo(DATA_DIR, DATA_FILE, datas);

        var listInstance = (IList)typeof(List<>)
          .MakeGenericType(objectsLists[selectedData].GetType().GetGenericArguments()[0])
          .GetConstructor(System.Type.EmptyTypes)
          .Invoke(null);


        foreach (var item in objectsLists[selectedData]) {
            listInstance.Add(item);
        }

        object[] parameters = new object[3];
        parameters[0] = RESOURCES_DIR;
        parameters[1] = datas[selectedData].directory; //ResourcesLoader.LOADER_DIR + MansionRoomMng.ROOMS_F;
        parameters[2] = listInstance;
        
        System.Reflection.MethodInfo method = typeof(XmlReader).GetMethod("SaveXmlTo");
        System.Reflection.MethodInfo generic = method.MakeGenericMethod(objectsLists[selectedData].GetType().GetGenericArguments()[0]);
        generic.Invoke(null, parameters);

        //XmlReader.SaveXml("Assets/Resources/" + ResourcesLoader.LOADER_DIR + MansionRoomMng.ROOMS_F, list);

        AssetDatabase.Refresh();
    }


    void readData() {

        selectedData = 0;
        lastSelectedData = -1;

        objectsLists = new List<IList>();

        //DataString removeData = null;

        List<DataString> removeData = new List<DataString>();

        foreach (var data in datas) {
            if (data.directory == "") {
                removeData.Add(data);
                //Debug.LogError("No directory ");
                continue;
            }
            try {
                object[] parameters = new object[1];
                parameters[0] = RESOURCES_DIR + data.directory;

                System.Reflection.MethodInfo method = typeof(XmlReader).GetMethod("LoadXMLFrom");
                System.Reflection.MethodInfo generic = method.MakeGenericMethod(System.Type.GetType(data.className + ",Assembly-CSharp"));

                IList list = (IList)generic.Invoke(null, parameters);
                
                /*foreach (var item in list.GetType().GetGenericArguments()[0].GetFields()) {
                    data.addFieldOptions(item.Name, item.FieldType.Name);
                }*/

                objectsLists.Add(list);
            } catch {
                removeData.Add(data);
                Debug.LogError(data.className + " Could not be loaded");
            }
        }

        foreach (var item in removeData) {
            datas.Remove(item);
        }
        
        if(objectsLists.Count > 0)
            changeSelectedData();



        selectedElement = 0;
        
    }


    void createElement(object data, IList list, int id) {

        GUILayout.BeginVertical("Box");
        
        foreach (System.Reflection.FieldInfo item in fields) {
            createField(item, data, id, ref list);
        }

        GUILayout.EndScrollView();
    }

    void createField<T>(System.Reflection.FieldInfo itemvar, T data, int id, ref IList list, int showName = -1 ) {
        if (itemvar.IsStatic) return;
       // if (datas[selectedData].getFieldOptions(itemvar.Name).show == false) return;
        
        string name = itemvar.Name;
        if (showName != -1) name = showName.ToString() + " " + itemvar.FieldType.Name;

        object value =  itemvar.GetValue(data);

        if (value == null) {
            //Debug.Log(itemvar);
            value = initVoidValue(itemvar, value);
            //Debug.Log(itemvar + "  -2-  " + value);
            //Debug.Log(value);
            if (value == null) return;
        }


        GUILayout.BeginHorizontal();


        if (itemvar.FieldType.IsGenericType && itemvar.FieldType.GetGenericTypeDefinition() == typeof(List<>)) {

            object objRef = list[id];

            if (itemvar.FieldType.GetGenericArguments()[0] == typeof(string)){
                createFieldListString(name, ref value, false, getTypeReference(itemvar), getRefenrece(itemvar));
            } else {
                createFieldtList(name, ref value, false);
            }

            // Debug.Log((value as IList).Count);
            try {
                itemvar.SetValue(objRef, value);
                list[id] = (T)objRef;
            } catch {

            }

        } else if (itemvar.FieldType.IsArray) {

            //createFieldtList(itemvar.Name, value, id, ref list);
            object objRef = list[id];

            if (itemvar.FieldType.GetElementType() == typeof(string)) {
                createFieldListString(name, ref value, true, getTypeReference(itemvar), getRefenrece(itemvar));
                string[] s = new string[(value as List<string>).Count];
                for (int i = 0; i < s.Length; i++) {
                    s[i] = (value as List<string>)[i];
                }
                itemvar.SetValue(objRef, s);
                //Debug.Log(value);
            } else {
                createFieldtList(name, ref value, true);
                itemvar.SetValue(objRef, value);
            }


            list[id] = (T)objRef;
           
        } else {
            
            GUILayout.Label(name, GUILayout.Width(100));

            object objRef = list[id];


            if (itemvar.FieldType.IsClass && itemvar.FieldType != typeof(string)) {
                createClassField(itemvar, value);
                itemvar.SetValue(objRef, value);

            } else if (itemvar.FieldType.IsEnum) {
                System.Enum es = (System.Enum)value;
                es = EditorGUILayout.EnumPopup(es, GUILayout.Width(100));
                itemvar.SetValue(objRef, es);

            }else if (!createGenericField(itemvar, value, objRef)){
                //object r = itemvar.GetValue(objRef);
                createClassField(itemvar, value);
                itemvar.SetValue(objRef, value);
            }
            
            list[id] = (T)objRef;
        }


        GUILayout.EndHorizontal();
    }

    object initVoidValue(System.Reflection.FieldInfo itemvar, object value) {
        if (itemvar.FieldType == typeof(string)) {
            value = "";
        } else if (itemvar.FieldType.IsArray) {
            value = System.Array.CreateInstance(itemvar.FieldType.GetElementType(), 0);
        } else if (itemvar.FieldType.IsGenericType && itemvar.FieldType.GetGenericTypeDefinition() == typeof(List<>)) {
            if(itemvar.FieldType.GetGenericArguments()[0] == typeof(string))
                value = new List<string>();
            else
                value = (IList)System.Activator.CreateInstance(itemvar.FieldType, 0);
        } else {
            value = System.Activator.CreateInstance(itemvar.FieldType);
        }

        if (value == null) {
            Debug.LogError("Could not create instance of " + itemvar.FieldType);
        }

        return value;

    }

    bool createGenericField(System.Reflection.FieldInfo itemvar, object value, object objRef) {

                
        if (itemvar.FieldType == typeof(int)) {
            var v = EditorGUILayout.IntField(System.Convert.ToInt32(value), GUILayout.Width(100));
            itemvar.SetValue(objRef, v);            
            return true;

        } else if (itemvar.FieldType == typeof(float)) {
            var v = EditorGUILayout.FloatField(System.Convert.ToSingle(value), GUILayout.Width(100)); //GUILayout.TextField(value.ToString(), GUILayout.Width(100));
            itemvar.SetValue(objRef, v);            
            return true;

        } else if (itemvar.FieldType == typeof(double)) {
            var v = EditorGUILayout.DoubleField(System.Convert.ToDouble(value), GUILayout.Width(100)); //GUILayout.TextField(value.ToString(), GUILayout.Width(100));
            itemvar.SetValue(objRef, v);
            return true;

        } else if (itemvar.FieldType == typeof(string)) {
            string v;

            System.Type typeRef = getTypeReference(itemvar);
            string reference = getRefenrece(itemvar);

            if (typeRef != null) {
                v = TypeReferenceField(value, typeRef);
            } else if (reference != null) {
                v = ReferenceField(value, reference);

            } else {
                v = EditorGUILayout.TextField(value.ToString(), GUILayout.Width(300));
            }
            itemvar.SetValue(objRef, v);
            // Debug.Log(itemvar + "  -1-  " + itemvar.GetValue(objRef));
            return true;

        } else if (itemvar.FieldType == typeof(bool)) {

            bool b = GUILayout.Toggle(System.Convert.ToBoolean(value), "");
            itemvar.SetValue(objRef, b);
            return true;
        } 

        return false;
    }
    

    string ReferenceField(object value, string referenceName) {

        string[] names = getElementsNamesOf(referenceName);
        int r = 0;

        for (int i = 0; i < names.Length; i++) {
            if (value.ToString() == names[i]) {
                r = i;
                break;
            }
        }
        r = EditorGUILayout.Popup(r, names, GUILayout.Width(100));
        return names[r];
    }

    string TypeReferenceField(object value, System.Type referenceType) {
        string[] listNames = getListNamesOf(referenceType);
       // int l = 0;
        int e = 0;

       // l = EditorGUILayout.Popup(l, listNames, GUILayout.Width(100));

        List<string> elementNames = new List<string>();
        foreach (var item in listNames) {
            elementNames.AddRange(getElementsNamesOf(item));
        }
        for (int i = 0; i < elementNames.Count; i++) {
            if (value.ToString() == elementNames[i]) {
                e = i;
                break;
            }
        }

        e = EditorGUILayout.Popup(e, elementNames.ToArray(), GUILayout.Width(100));

        return elementNames[e];
    }

    string getRefenrece(System.Reflection.FieldInfo itemvar) {

        object[] attributes = itemvar.GetCustomAttributes(false);
        foreach (var item in attributes) {
            if (item.GetType() == typeof(XmlReference)) {
                if (isValidRefenrence(item.ToString())) {
                    return item.ToString();
                }
                break;
            }
        }

        return null;
    }

    System.Type getTypeReference(System.Reflection.FieldInfo itemvar) {

        object[] attributes = itemvar.GetCustomAttributes(false);
        foreach (var item in attributes) {
            if (item.GetType() == typeof(XmlTypeReference)) {
                XmlTypeReference x = item as XmlTypeReference;
                if (isValidTypeRefenrence(x.reference)) {
                    return x.reference;
                }
                break;
            }
        }

        return null;
    }

    bool isValidRefenrence(string reference) {
        foreach (var item in datas) {
            if (reference == item.displayName) return true;
        }
        return false;
    }
    bool isValidTypeRefenrence(System.Type reference) {
        foreach (var item in datas) {
            if (reference.ToString() == item.className) return true;
        }
        return false;
    }

    string[] getElementsNamesOf(string name) {

        int d = 0;
        for (int i = 0; i < datas.Count; i++) {
            if(datas[i].displayName == name) {
                d = i;
                break;
            }
        }

        string[] list = new string[objectsLists[d].Count];

        System.Reflection.FieldInfo field;
        object val;
        for (int i = 0; i < list.Length; i++) {
            field = objectsLists[d].GetType().GetGenericArguments()[0].GetField(datas[d].variableID);
            if (field != null) {
                val = field.GetValue(objectsLists[d][i]);
                if (val != null)
                    list[i] = val.ToString();
            }
            //list[i] = objectsLists[d].GetType().GetGenericArguments()[0].GetField(datas[d].variableID).GetValue(objectsLists[d][i]).ToString();

        }

        return list;
    }
    string[] getListNamesOf(System.Type type) {

        List<string> s = new List<string>();
        foreach (var item in datas) {
            if (type.ToString() == item.className)
                s.Add(item.displayName);
        }

        return s.ToArray();

    }

    void createClassField(System.Reflection.FieldInfo itemvar, object objRef) {
        IList l = new List<object>();
        l.Add(objRef);


        GUILayout.EndHorizontal();
        GUIStyle verticalStyle = new GUIStyle("box");
        verticalStyle.margin.left = 25;
        GUILayout.BeginVertical(verticalStyle);
        
        foreach (var item in itemvar.FieldType.GetFields()) {
            if (item.FieldType == itemvar.FieldType) continue;
            createField(item, objRef, 0, ref l);
        }
        GUILayout.EndVertical();
        GUILayout.BeginHorizontal();
    }
    
    void createFieldtList(string listName, ref object value, bool isArray) {
        //Debug.Log(listName + "    ++   " + value.GetType());
        IList il = (value as IList);

        //if (il == null) return;
                
        GUILayout.EndHorizontal();
        

        GUIStyle verticalStyle = new GUIStyle("box");        
        GUILayout.BeginVertical(verticalStyle);


        GUILayout.BeginHorizontal();
        GUILayout.Label(listName, GUILayout.Width(100));
        GUILayout.Label("Amount: " + il.Count, GUILayout.Width(100));

        if(GUILayout.Button("+", miniButtonStyle)) {
            if (isArray) 
                addFieldToArray(ref il, il.GetType().GetElementType());
            else
                addFieldToList(ref il);            
        }

        GUILayout.EndHorizontal();

        for (int i = 0; i < il.Count; i++) {
            //foreach (var item in il) {

            GUILayout.BeginHorizontal(verticalStyle);
            
            if(GUILayout.Button("-", miniButtonStyle)){
                if (isArray) {
                    removeFieldOnArray(ref il, i);
                    break;
                } else {
                    removeFieldOnList(ref il, i);
                    break;
                }

            }

            GUILayout.BeginVertical();

            if (il[i] == null) break;

            if (il[i].GetType().IsClass && !il[i].GetType().IsGenericType/* && il[i].GetType().GetGenericTypeDefinition() != typeof(List<>)*/) {
                foreach (var item in il[i].GetType().GetFields()) {
                    createField(item, il[i], i, ref il);
                }
            } else {
                var genericObject = System.Activator.CreateInstance(typeof(AuxObj<>).MakeGenericType(il[i].GetType()), il[i]);

                foreach (System.Reflection.FieldInfo itemvar in genericObject.GetType().GetFields()) {
                    createField(itemvar, genericObject, i, ref il, i);
                }
            }
            
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();

        GUILayout.BeginHorizontal();

        value = il;

    }

    void createFieldListString(string listName, ref object value, bool isArray, System.Type typeReference, string reference) {
                
        List<string> stringList = (value as List<string>);

        if (isArray) {
            string[] stringArray = (value as string[]);
            stringList = new List<string>();
            foreach (var item in stringArray) {
                stringList.Add(item);
            }
        }
        

        GUILayout.EndHorizontal();

        int id = 0;

        GUIStyle verticalStyle = new GUIStyle("box");

        GUILayout.BeginVertical(verticalStyle);

        GUILayout.BeginHorizontal();
        GUILayout.Label(listName, GUILayout.Width(100));
        GUILayout.Label("Amount: " + stringList.Count, GUILayout.Width(100));

        if (GUILayout.Button("+", miniButtonStyle)) {
            addFieldToListString(ref stringList);
        }

        GUILayout.EndHorizontal();

        for (int i = 0; i < stringList.Count; i++) {

            GUILayout.BeginHorizontal(verticalStyle);

            if (GUILayout.Button("-", miniButtonStyle)) {
                removeFieldOnListString(ref stringList, i);
                break;
            }

            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();

            GUILayout.Label(i.ToString(), GUILayout.Width(100));

            if (typeReference != null) {
                stringList[i] = TypeReferenceField(stringList[i], typeReference);
            } else if (reference != null) {
                stringList[i] = ReferenceField(stringList[i], reference);
            } else {
                stringList[i] = GUILayout.TextField(stringList[i], GUILayout.Width(100));
            }

            GUILayout.EndHorizontal();

            id++;
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();

        GUILayout.BeginHorizontal();

        value = stringList;
    }

    void addFieldToList(ref IList list) {
        var item = System.Activator.CreateInstance(list.GetType().GetGenericArguments()[0]);
        list.Add(item);
    }
    void addFieldToListString(ref List<string> list) {
        list.Add("");
    }
    
    void removeFieldOnList(ref IList list, int id) {
        list.RemoveAt(id);
    }
    void removeFieldOnListString(ref List<string> list, int id) {
        list.RemoveAt(id);
    }

    void addFieldToArray(ref IList array, System.Type type) {
        object[] aux = new object[array.Count + 1];

        for (int i = 0; i < array.Count; i++) {
            aux[i] = array[i];
        }

        if (type == typeof(string))
            aux[array.Count] = "";
        else if (type.IsArray)
            aux[array.Count] = System.Array.CreateInstance(type, aux.Length);
        else
            aux[array.Count] = System.Activator.CreateInstance(type);

        array = (IList)System.Activator.CreateInstance(array.GetType(), aux.Length);
        for (int i = 0; i < aux.Length; i++) {
            array[i] = aux[i]; //System.Convert.ChangeType(aux[i], type);
        }
    }

    void removeFieldOnArray(ref IList array, int id) {
        object[] aux = new object[array.Count - 1];

        int re = 0;

        for (int i = 0; i < array.Count; i++) {
            if (i == id) {
                re = 1;
                continue;
            }
            aux[i - re] = array[i];
        }
        array = (IList)System.Activator.CreateInstance(array.GetType(), aux.Length);
        for (int i = 0; i < aux.Length; i++) {
            array[i] = aux[i];
        }
    }

    object[] intToObjectArray(int[] arrayInt) {
        object[] res = new object[arrayInt.Length];
        for (int i = 0; i < arrayInt.Length; i++) {
            res[i] = arrayInt[i];
        }

        return res;
    }



    public class DataString {

        public const string SAVE_DIR = "Assets/Resources/";

        [XmlAttribute("displayName")]
        public string displayName;

        public string className;
        public string variableID;
        public string directory;

        //public List<FieldOptions> fields;

        public DataString() {
            displayName = "";
            className = "";
            variableID = "";
            directory = "";
        }

        public DataString(string displayName, string className, string id, string directory) {
            this.displayName = displayName;
            this.className = className;
            this.variableID = id;
            this.directory = directory;
        }

        /*public void addFieldOptions(string name, string type) {

            Debug.Log(name);
            foreach (var item in fields) {
                if (item.name == name) return;
            }
            
            fields.Add(new FieldOptions(name, type));

        }

        public FieldOptions getFieldOptions(string name) {

            foreach (var item in fields) {
                if (item.name == name) return item;
            }

            return null;
        }*/

        /*public class FieldOptions {

            public const string NO_REFENRENCE = "--NONE--";

            [XmlAttribute("fieldName")]
            public string name;
            public string type;
            public bool show = true;

            public string reference = NO_REFENRENCE;

            public FieldOptions() {
                name = "NULL";
            }

            public FieldOptions(string name, string type) {
                this.name = name;
                this.type = type;
            }

            public bool canReference() {
                if (type == "String") return true;

                return false;
            }


        }*/

    }

    class AuxObj<T> {

        public T aux;

        public AuxObj(T aux) {
            this.aux = aux;
        }

        public override string ToString() {
            return aux.ToString();
        }
    }
}
