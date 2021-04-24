function goToPath(path) {
    if (!path)
        window.location.href = "/?p=" + encodeURIComponent($("#pathInput")[0].value);
    else
        window.location.href = "/?p=" + encodeURIComponent(path.replace("//", "/"));
}