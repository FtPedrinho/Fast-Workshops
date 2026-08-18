import Chart from "chart.js/auto";

import {
    getColaboradores,
    getWorkshops,
    getWorkshopById
} from "../services/api.js";

import { renderNavbar } from "../components/Navbar.js";

let participationChart = null;
let workshopChart = null;

async function renderDashboard(app) {
    app.innerHTML = `
        ${renderNavbar()}

        <main class="container dashboard">

            <header class="page-header">
                <div>
                    <h1>Dashboard</h1>

                    <p>
                        Análise da participação dos colaboradores
                        nos workshops da FAST Soluções.
                    </p>
                </div>
            </header>

            <section
                id="dashboard-summary"
                class="dashboard-summary"
            >
                <p>Carregando métricas...</p>
            </section>

            <section class="charts-grid">

                <article class="chart-card">
                    <header class="chart-header">
                        <h2>Participação por colaborador</h2>

                        <p>
                            Quantidade de workshops em que cada
                            colaborador participou.
                        </p>
                    </header>

                    <div class="chart-container">
                        <canvas id="participation-chart"></canvas>
                    </div>
                </article>

                <article class="chart-card">
                    <header class="chart-header">
                        <h2>Participantes por workshop</h2>

                        <p>
                            Distribuição de participantes entre
                            os workshops.
                        </p>
                    </header>

                    <div class="chart-container">
                        <canvas id="workshop-chart"></canvas>
                    </div>
                </article>

            </section>

        </main>
    `;

    await loadDashboard();
}

async function loadDashboard() {
    const summaryContainer = document.querySelector(
        "#dashboard-summary"
    );

    try {
        const data = await loadDashboardData();

        renderSummary(summaryContainer, data);

        renderParticipationChart(data);
        renderWorkshopChart(data);

    } catch (error) {
        renderDashboardError(
            summaryContainer,
            error.message
        );
    }
}

async function loadDashboardData() {
    const [colaboradores, workshops] = await Promise.all([
        getColaboradores(),
        getWorkshops()
    ]);

    const workshopsWithParticipants = await Promise.all(
        workshops.map(workshop =>
            getWorkshopById(workshop.id)
        )
    );

    return {
        colaboradores,
        workshops: workshopsWithParticipants
    };
}

function renderSummary(container, data) {
    const totalParticipations = data.workshops.reduce(
        (total, workshop) =>
            total + (workshop.participantes?.length ?? 0),
        0
    );

    container.innerHTML = `
        <article class="summary-card">
            <span class="summary-label">
                Workshops
            </span>

            <strong class="summary-value">
                ${data.workshops.length}
            </strong>
        </article>

        <article class="summary-card">
            <span class="summary-label">
                Colaboradores
            </span>

            <strong class="summary-value">
                ${data.colaboradores.length}
            </strong>
        </article>

        <article class="summary-card">
            <span class="summary-label">
                Participações
            </span>

            <strong class="summary-value">
                ${totalParticipations}
            </strong>
        </article>
    `;
}

function renderParticipationChart(data) {
    const canvas = document.querySelector(
        "#participation-chart"
    );

    const participationByCollaborator =
        calculateParticipationByCollaborator(data);

    destroyParticipationChart();

    participationChart = new Chart(canvas, {
        type: "bar",

        data: {
            labels: participationByCollaborator.labels,

            datasets: [
                {
                    label: "Workshops participados",

                    data: participationByCollaborator.values,

                    borderWidth: 1
                }
            ]
        },

        options: {
            responsive: true,

            maintainAspectRatio: false,

            plugins: {
                legend: {
                    display: false
                }
            },

            scales: {
                y: {
                    beginAtZero: true,

                    ticks: {
                        precision: 0
                    }
                }
            }
        }
    });
}

function renderWorkshopChart(data) {
    const canvas = document.querySelector(
        "#workshop-chart"
    );

    const participantsByWorkshop =
        calculateParticipantsByWorkshop(data);

    destroyWorkshopChart();

    workshopChart = new Chart(canvas, {
        type: "pie",

        data: {
            labels: participantsByWorkshop.labels,

            datasets: [
                {
                    label: "Participantes",

                    data: participantsByWorkshop.values,

                    borderWidth: 1
                }
            ]
        },

        options: {
            responsive: true,

            maintainAspectRatio: false
        }
    });
}

function calculateParticipationByCollaborator(data) {
    const participationCount = new Map();

    data.colaboradores.forEach(colaborador => {
        participationCount.set(
            colaborador.id,
            {
                nome: colaborador.nome,
                quantidade: 0
            }
        );
    });

    data.workshops.forEach(workshop => {
        const participants =
            workshop.participantes ?? [];

        participants.forEach(participante => {
            const collaborator =
                participationCount.get(participante.id);

            if (collaborator) {
                collaborator.quantidade++;
            }
        });
    });

    const entries = Array.from(
        participationCount.values()
    );

    return {
        labels: entries.map(entry => entry.nome),

        values: entries.map(
            entry => entry.quantidade
        )
    };
}

function calculateParticipantsByWorkshop(data) {
    return {
        labels: data.workshops.map(
            workshop => workshop.nome
        ),

        values: data.workshops.map(
            workshop =>
                workshop.participantes?.length ?? 0
        )
    };
}

function destroyParticipationChart() {
    if (participationChart) {
        participationChart.destroy();
        participationChart = null;
    }
}

function destroyWorkshopChart() {
    if (workshopChart) {
        workshopChart.destroy();
        workshopChart = null;
    }
}

function renderDashboardError(container, message) {
    container.innerHTML = `
        <div class="error-state">

            <h2>
                Não foi possível carregar as métricas.
            </h2>

            <p>
                ${message}
            </p>

        </div>
    `;
}

export {
    renderDashboard
};