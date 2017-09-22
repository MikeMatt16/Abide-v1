function valueChanged(id) {
    var Element = document.getElementById(id);
    var uid = Element.getAttribute("uid");
    var value = document.getElementById("valueInput" + uid).value;
    window.external.SetValue(uid, value);
}

function setValue(id, value) {
    var Element = document.getElementById(id);
    var uid = Element.getAttribute("uid");
    document.getElementById("valueInput" + uid).value = value;
}

function blockChanged(id) {
    var Element = document.getElementById(id);
    var uid = Element.getAttribute("uid");
    var select = document.getElementById("valueSelect" + uid);
    var value = select.options[select.selectedIndex].getAttribute("value");
    window.external.SetValue(uid, value);
}

function enumChanged(id) {
    var Element = document.getElementById(id);
    var uid = Element.getAttribute("uid");
    var select = document.getElementById("enumSelect" + uid);
    var value = select.options[select.selectedIndex].getAttribute("value");
    window.external.SetEnum(uid, value);
}

function setReflexive(id, reflexiveName, count) {
    var Element = document.getElementById(id);
    while (Element.length > 0)
        Element.remove(0);

    for (var i = 0; i < count; i++) {
        var option = document.createElement("option");
        option.value = i;
        option.innerText = i + ": " + reflexiveName;
        Element.appendChild(option);
    }

    if (count > 0)
        window.external.SetReflexive(Element.getAttribute("uid"), Element.selectedIndex);
}

function setReflexiveName(id, name, index) {
    document.getElementById(id).options[index].text = name;
}

function selectBlock(id, value) {
    var Element = document.getElementById(id);
    var success = false;
    for (var i = 0; i < Element.length; i++) {
        if (Element.options[i].value == value) {
            Element.selectedIndex = i;
            success = true;
            break;
        }
    }

    if (!success) {
        var newOption = document.createElement("option");
        newOption.value = value;
        newOption.text = value + ": Value too large or too small to be the indexer";
        Element.appendChild(newOption);
    }

    sortSelect(id);
}

function selectEnum(id, value) {
    var Element = document.getElementById(id);
    var success = false;
    for (var i = 0; i < Element.length; i++) {
        if (Element.options[i].value == value) {
            Element.selectedIndex = i;
            success = true;
            break;
        }
    }

    if (!success) {
        var newOption = document.createElement("option");
        newOption.value = value;
        newOption.text = value + ": Value too large or too small to be the indexer";
        Element.appendChild(newOption);
    }

    sortSelect(id);
}

function addEnum(id, value, name) {
    var Element = document.getElementById(id);
    var success = false;
    for (var i = 0; i < Element.length; i++) {
        if (Element.options[i].value == value) {
            document.getElementById(id).options[i].text = name;
            success = true;
            break;
        }
    }

    if (!success) {
        var newOption = document.createElement("option");
        newOption.value = value;
        newOption.setAttribute("value", value);
        newOption.text = name;
        document.getElementById(id).appendChild(newOption);
    }

    sortSelect(id);
}

function sortSelect(id) {
}

function tagButtonPress(id) {
    var Element = document.getElementById(id);
    window.external.TagButtonClick(Element.getAttribute("uid"), Element.getAttribute("ident"));
}

function tagAssignIdent(id, ident, tagClass, tagPath) {
    document.getElementById(id).setAttribute("ident", ident);
    var uid = document.getElementById(id).getAttribute("uid");
    var tagInputId = "tagInput" + uid;
    document.getElementById(tagInputId).setAttribute("value", tagClass + " - " + tagPath);
}

function bitmaskChanged(bitmaskId) {
    var Element = document.getElementById(bitmaskId);
    var uid = Element.getAttribute("uid");
    var bits = Element.getAttribute("bits");

    var Value = 0;
    for (var i = 0; i < bits; i++) {
        if (document.getElementById("bit" + i + "-" + uid).checked)
            Value |= Math.pow(2, i);
    }
    window.external.SetBitmask(uid, Value);
}

function checkBit(uid, bit, checked) {
    if (checked) {
        document.getElementById("bit" + bit + "-" + uid).setAttribute("checked", true);
    }
    else {
        document.getElementById("bit" + bit + "-" + uid).removeAttribute("checked");
    }
}

function nameBit(uid, bit, name) {
    document.getElementById("bit-label" + bit + "-" + uid).innerText = name;
}

function revealBit(bitmaskId, bit, bits) {
    document.getElementById(bitmaskId).setAttribute("bits", bits);
    var uid = document.getElementById(bitmaskId).getAttribute("uid");
    var bitId = "bit" + bit + "-" + uid;
    var lblId = "bit-label" + bit + "-" + uid;
    document.getElementById(bitId).className = "bit-visible";
    document.getElementById(lblId).className = "bit-visible";
}

function stringIdButtonPress(id) {
    var Element = document.getElementById(id);
    window.external.StringIDButtonClick(Element.getAttribute("uid"), Element.getAttribute("sid"));
}

function stringIdAssignID(id, sid, stringName) {
    document.getElementById(id).setAttribute("sid", sid);
    var uid = document.getElementById(id).getAttribute("uid");
    var stringIdInputId = "stringIdInput" + uid;
    document.getElementById(stringIdInputId).setAttribute("value", stringName);
}

function stringAssign(id, string) {
    var uid = document.getElementById(id).getAttribute("uid");
    var stringInputId = "stringInput" + uid;
    document.getElementById(stringInputId).innerText = string;
}

function stringChanged(id) {
    var uid = document.getElementById(id).getAttribute("uid");
    var stringInputId = "stringInput" + uid;
    window.external.SetString(uid, document.getElementById(stringInputId).innerText);
}

function unicodeChanged(id) {
    var uid = document.getElementById(id).getAttribute("uid");
    var stringInputId = "unicodeInput" + uid;
    window.external.SetUnicode(uid, document.getElementById(stringInputId).innerText);
}

function reflexiveChunkChanged(id) {
    var Element = document.getElementById(id);
    window.external.SetReflexive(Element.getAttribute("uid"), Element.selectedIndex);
}