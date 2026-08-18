// Altere para o endereço correto da sua API.
const API_BASE_URL = "http://localhost:5000/api";

async function request(endpoint, options = {}) {
    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
        headers: {
            "Content-Type": "application/json"
        },
        ...options
    });

    if (!response.ok) {
        throw new Error(await getErrorMessage(response));
    }

    if (response.status === 204) {
        return null;
    }

    return response.json();
}

async function getErrorMessage(response) {
    try {
        const error = await response.json();

        return error.message || "Ocorreu um erro na comunicação com a API.";
    } catch {
        return "Ocorreu um erro na comunicação com a API.";
    }
}

// COLABORADORES
function getColaboradores() {
    return request("/colaboradores");
}

function getColaboradorById(id) {
    return request(`/colaboradores/${id}`);
}

function createColaborador(nome) {
    return request("/colaboradores", {
        method: "POST",
        body: JSON.stringify({
            nome
        })
    });
}

function updateColaborador(id, nome) {
    return request(`/colaboradores/${id}`, {
        method: "PUT",
        body: JSON.stringify({
            nome
        })
    });
}

function deleteColaborador(id) {
    return request(`/colaboradores/${id}`, {
        method: "DELETE"
    });
}


// WORKSHOPS
function getWorkshops() {
    return request("/workshops");
}

function getWorkshopById(id) {
    return request(`/workshops/${id}`);
}

function createWorkshop(workshop) {
    return request("/workshops", {
        method: "POST",
        body: JSON.stringify(workshop)
    });
}

function updateWorkshop(id, workshop) {
    return request(`/workshops/${id}`, {
        method: "PUT",
        body: JSON.stringify(workshop)
    });
}

function deleteWorkshop(id) {
    return request(`/workshops/${id}`, {
        method: "DELETE"
    });
}


// PARTICIPAÇÕES
function getParticipacoes(workshopId) {
    return request(`/workshops/${workshopId}/participacoes`);
}

function createParticipacao(workshopId, colaboradorId) {
    return request(`/workshops/${workshopId}/participacoes`, {
        method: "POST",
        body: JSON.stringify({
            colaboradorId
        })
    });
}

function deleteParticipacao(workshopId, colaboradorId) {
    return request(
        `/workshops/${workshopId}/participacoes/${colaboradorId}`,
        {
            method: "DELETE"
        }
    );
}


export {
    getColaboradores,
    getColaboradorById,
    createColaborador,
    updateColaborador,
    deleteColaborador,

    getWorkshops,
    getWorkshopById,
    createWorkshop,
    updateWorkshop,
    deleteWorkshop,

    getParticipacoes,
    createParticipacao,
    deleteParticipacao
};