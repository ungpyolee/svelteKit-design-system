export const menuConfig = [
    {
            type: "dropdown-menu",
            name: "dashboard",
            innerText: "Dashboard",
            lucideIcon: "LayoutDashboard",
            submenuList: [
                {
                    name: "home",
                    innerText: "Home",
                    url: "/dashboard/home",
                }
            ]
        },
        {
            type: "title",
            innerText: "UI Elements",
        },
        {
            type: "dropdown-menu",
            name: "components",
            innerText: "Components",
            lucideIcon: "FileText",
            submenuList: [
                {
                    name: "typography",
                    innerText: "Typography",
                    url: "/components/typography",
                },
                {
                    name: "card",
                    innerText: "Card",
                    url: "/components/card",
                }
            ]
        },
        {
            type: "dropdown-menu",
            name: "forms",
            innerText: "Forms",
            lucideIcon: "Form",
            submenuList: [
                {
                    name: "form",
                    innerText: "Form",
                    url: "/forms/form",
                },
            ]
        },
        {
            type: "dropdown-menu",
            name: "tables",
            innerText: "Tables",
            lucideIcon: "TableProperties",
            submenuList: [
                {
                    name: "basic-table",
                    innerText: "Basic Table",
                    url: "/tables/basic-table",
                },
            ]
        },
        {
            type: "title",
            innerText: "Pages",
        },
        {
            type: "dropdown-menu",
            name: "rms",
            innerText: "RMS",
            lucideIcon: "ChartNetwork",
            submenuList: [
                {
                    name: "page-1",
                    innerText: "Page-1",
                    url: "/rms/page-1",
                },
            ]
        },
        {
            type: "dropdown-menu",
            name: "auth",
            innerText: "Auth",
            lucideIcon: "LockKeyhole",
            submenuList: [
                {
                    name: "signin",
                    innerText: "Sign in",
                    url: "/auth/signin",
                },
            ]
        },
        {
            type: "dropdown-menu",
            name: "setting",
            innerText: "Setting",
            lucideIcon: "Cog",
            submenuList: [
                {
                    name: "team",
                    innerText: "Team",
                    url: "/setting/team",
                },
            ]
        },
        {
            type: "menu",
            name: "test",
            innerText: "Test",
            url: "/test",
            lucideIcon: "FileText",
        },
]