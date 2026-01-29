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
        innerText: "Admin",
    },
    {
        type: "dropdown-menu",
        name: "admin-users",
        innerText: "Users",
        lucideIcon: "Users",
        submenuList: [
            {
                name: "users",
                innerText: "사용자 목록",
                url: "/users",
            },
            {
                name: "new",
                innerText: "사용자 추가",
                url: "/users/new",
            },
        ]
    },
    {
        type: "dropdown-menu",
        name: "admin-permissions",
        innerText: "Permissions",
        lucideIcon: "Shield",
        submenuList: [
            {
                name: "permissions",
                innerText: "권한 목록",
                url: "/permissions",
            },
            {
                name: "new",
                innerText: "권한 추가",
                url: "/permissions/new",
            },
        ]
    },
    {
        type: "dropdown-menu",
        name: "admin-tenants",
        innerText: "Tenants",
        lucideIcon: "Building2",
        submenuList: [
            {
                name: "tenants",
                innerText: "테넌트 목록",
                url: "/tenants",
            },
            {
                name: "new",
                innerText: "테넌트 추가",
                url: "/tenants/new",
            },
        ]
    },
    {
        type: "title",
        innerText: "Apps",
    },
    {
        type: "dropdown-menu",
        name: "cms",
        innerText: "CMS",
        lucideIcon: "FileText",
        submenuList: [
            {
                name: "spaces",
                innerText: "Spaces",
                url: "/cms/spaces",
            },
            {
                name: "contents",
                innerText: "Contents",
                url: "/cms/contents",
            },
            {
                name: "tags",
                innerText: "Tags",
                url: "/cms/tags",
            },
            {
                name: "topics",
                innerText: "Topics",
                url: "/cms/topics",
            },
        ]
    },
    {
        type: "dropdown-menu",
        name: "rms",
        innerText: "RMS",
        lucideIcon: "ChartNetwork",
        submenuList: [
            {
                name: "user-dashboard",
                innerText: "기여자 대시보드",
                url: "/rms/user-dashboard",
            },
            {
                name: "digital-assets",
                innerText: "기술자료",
                url: "/rms/digital-assets",
            },
            {
                name: "new",
                innerText: "새 기술자료 등록",
                url: "/rms/digital-assets/new",
            },
            {
                name: "digital-asset-requests",
                innerText: "기술자료 요청",
                url: "/rms/digital-asset-requests",
            },
            {
                name: "digital-asset-payments",
                innerText: "판매내역",
                url: "/rms/digital-asset-payments",
            }
        ]
    },
    {
        type: "dropdown-menu",
        name: "rms-admin",
        innerText: "RMS 관리",
        lucideIcon: "Settings",
        submenuList: [
            {
                name: "dashboard",
                innerText: "관리자 대시보드",
                url: "/rms-admin/dashboard",
            },
            {
                name: "digital-assets",
                innerText: "기술자료 관리",
                url: "/rms-admin/digital-assets",
            },
            {
                name: "asset-types",
                innerText: "자료 타입 관리",
                url: "/rms-admin/asset-types",
            },
            {
                name: "motor-types",
                innerText: "전동기 타입 관리",
                url: "/rms-admin/motor-types",
            },
            {
                name: "inquiries",
                innerText: "문의사항",
                url: "/rms-admin/inquiries",
            },
            {
                name: "faq",
                innerText: "FAQ 관리",
                url: "/rms-admin/faq",
            },
        ]
    },
    {
        type: "title",
        innerText: "Pages",
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
        name: "data-registration",
        innerText: "자료 등록",
        url: "/data-registration",
        lucideIcon: "FileText",
    },
]