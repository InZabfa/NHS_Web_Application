function closeMessage(name) {
    var ctrl = $(".msg[data-msg=" + name + "]");
    if (ctrl !== null) {
        $(ctrl).click(function () {
            $(ctrl).remove();
        });
    }
}

function handleDialogs() {
    
}

function toggleExpandableControl(e, colAll) {
    var control = $(".expandable-outer[data-id='" + e + "']");
    var contents = $(".expandable-contents[data-id='" + e + "']");
    var collapsed = $(control).hasClass('collapsed');
    var opened = $(control).hasClass('expanded');
    if (colAll == true) {
        $(".expandable-outer").removeClass('expanded');
        $(".expandable-outer").addClass('collapsed');
    }
    if (collapsed) {
        $(control).removeClass("collapsed");
        $(control).addClass("expanded");
        return;
    }
    if (opened) {
        $(control).removeClass("expanded");
        $(control).addClass("collapsed");
        return;
    }
}