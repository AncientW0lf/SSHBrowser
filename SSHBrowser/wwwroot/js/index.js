function goToPath() {
    window.location.href = "/?p=" + encodeURIComponent($("#pathInput")[0].value);
}