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
                },
                {
                    name: "modal",
                    innerText: "Modal",
                    url: "/components/modal",
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
                {
                    name: "file-upload",
                    innerText: "File Upload",
                    url: "/forms/file-upload"
                }
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
                    name: "user-dashboard",
                    innerText: "User Dashboard",
                    url: "/rms/user-dashboard",
                },
                {
                    name: "digital-assets",
                    innerText: "Digital Assets",
                    url: "/rms/digital-assets",
                },
                {
                    name: "digital-asset-requests",
                    innerText: "Digital Asset Requests",
                    url: "/rms/digital-asset-requests",
                },
                {
                    name: "digital-asset-payments",
                    innerText: "Digital Asset payments",
                    url: "/rms/digital-asset-payments",
                }
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
                    name: "switch-team",
                    innerText: "Switch Team",
                    url: "/setting/switch-team",
                },
                {
                    name: "manage-teams",
                    innerText: "Manage Teams",
                    url: "/setting/manage-teams",
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