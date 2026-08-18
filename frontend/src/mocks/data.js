const colaboradores = [
    {
        id: 1,
        nome: "Ana Silva"
    },
    {
        id: 2,
        nome: "Carlos Oliveira"
    },
    {
        id: 3,
        nome: "Mariana Santos"
    },
    {
        id: 4,
        nome: "João Pereira"
    },
    {
        id: 5,
        nome: "Lucas Almeida"
    }
];

const workshops = [
    {
        id: 1,
        nome: "Clean Code",
        dataRealizacao: "2026-03-12",
        descricao: "Boas práticas para escrever código limpo, legível e sustentável.",
        participantes: [
            {
                id: 1,
                nome: "Ana Silva"
            },
            {
                id: 2,
                nome: "Carlos Oliveira"
            },
            {
                id: 3,
                nome: "Mariana Santos"
            },
            {
                id: 5,
                nome: "Lucas Almeida"
            }
        ]
    },
    {
        id: 2,
        nome: "Arquitetura de Software",
        dataRealizacao: "2026-06-11",
        descricao: "Conceitos fundamentais de arquitetura e organização de sistemas.",
        participantes: [
            {
                id: 1,
                nome: "Ana Silva"
            },
            {
                id: 3,
                nome: "Mariana Santos"
            },
            {
                id: 4,
                nome: "João Pereira"
            }
        ]
    },
    {
        id: 3,
        nome: "Testes Automatizados",
        dataRealizacao: "2026-08-13",
        descricao: "Estratégias e boas práticas para criação de testes automatizados.",
        participantes: [
            {
                id: 1,
                nome: "Ana Silva"
            },
            {
                id: 2,
                nome: "Carlos Oliveira"
            },
            {
                id: 4,
                nome: "João Pereira"
            },
            {
                id: 5,
                nome: "Lucas Almeida"
            }
        ]
    }
];

export {
    colaboradores,
    workshops
};

// Não é necessário implementar a entidade "Participação" no mock.