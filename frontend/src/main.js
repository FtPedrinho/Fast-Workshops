import { renderColaboradores } from "./pages/colaboradores.js";
import { renderWorkshops } from "./pages/workshops.js";
import { renderWorkshopDetalhes } from "./pages/workshopDetalhes.js";
import { renderDashboard } from "./pages/dashboard.js";

const app = document.querySelector("#app");

function renderPage() {
    const route = window.location.hash;

    if (route === "#/dashboard") {
        renderDashboard(app);
        return;
    }

    if (route === "#/colaboradores") {
        renderColaboradores(app);
        return;
    }

    if (route === "#/workshops") {
        renderWorkshops(app);
        return;
    }

    if (route.startsWith("#/workshops/")) {
        const workshopId = getWorkshopIdFromRoute(route);

        renderWorkshopDetalhes(app, workshopId);
        return;
    }

    navigateToWorkshops();
}

function getWorkshopIdFromRoute(route) {
    const routeParts = route.split("/");
    return Number(routeParts[2]);
}

function navigateToWorkshops() {
    window.location.hash = "#/workshops";
}

window.addEventListener("hashchange", renderPage);

renderPage();