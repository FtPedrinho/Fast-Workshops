function renderNavbar() {
    return `
        <nav class="navbar">
            <a href="#/workshops" class="navbar-brand">
                FAST Workshops
            </a>

            <div class="navbar-links">
                <a href="#/workshops">Workshops</a>
                <a href="#/colaboradores">Colaboradores</a>
                <a href="#/dashboard">Dashboard</a>
            </div>
        </nav>
    `;
}

export { renderNavbar };