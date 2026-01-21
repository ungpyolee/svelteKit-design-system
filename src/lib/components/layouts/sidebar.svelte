<script>
    import { sidebar, theme } from "$lib/stores";
    import { cn } from "$lib/utils";
    import { Logo } from "$lib/components/ui";
    import Icon from "$lib/icons/icon.svelte";
    import LogoUrl from '$lib/assets/logo.png';
    import LogoLightUrl from '$lib/assets/logo-light.png';
    import LogoSymbolUrl from '$lib/assets/logo-icon.png';
    import LogoSymbolLightUrl from '$lib/assets/logo-icon-light.png';
    import { menuConfig } from '$lib/config/menu.js';
    import { page } from '$app/state';
    let pathSegments = $derived(
        page.url.pathname.split('/').filter(Boolean)
    );
    let firstDepth = $derived(pathSegments[0] ?? "")
    let secondDepth = $derived(pathSegments[1] ?? "")

    let openMenus = $state(new Set());
    function toggleDropdown(name) {
        if (openMenus.has(name)) {
            // 이미 열려있으면 → 닫기
            openMenus.delete(name);
        } else {
            // 닫혀있으면 → 다른 거 다 닫고, 이것만 열기
            openMenus.clear();
            openMenus.add(name);
        }
        // Svelte 5에서 Set 변경을 감지시키려면 새 Set으로 재할당
        openMenus = new Set(openMenus);
    }

    $effect(() => {
        if (!firstDepth) return; 

        const currentMenu = menuConfig.find(m => m.name === firstDepth);
        if (currentMenu?.type === "dropdown-menu") {
            openMenus = new Set([firstDepth]);
        }
    });
</script>

<div 
    data-testid="layout-sidebar"
    class={cn("fixed border-r border-gray-100 dark:border-gray-800 left-0 top-0 bottom-0 h-screen z-50",
    "bg-white dark:bg-gray-900",
    $sidebar.expanded? "": "group"
)}>
    <nav class="flex flex-col w-full min-h-screen h-full">
        <div 
            data-testid="logo"
            class="py-3 px-5 border-b border-gray-100 dark:border-gray-800 h-[65px]">
            <a href="/" title="Logo 로고" class="w-full">
                {#if $sidebar.expanded}
                    {#if $theme === 'dark'}
                    <img src={LogoLightUrl} alt="Logo" width="168" height="40"/>
                    {:else}
                        <img src={LogoUrl} alt="Logo" width="168" height="40"/>
                    {/if}
                {:else}
                    {#if $theme === 'dark'}
                        <img src={LogoSymbolLightUrl} class="group-hover:hidden" alt="Logo" width="43" height="40"/>
                        <img src={LogoLightUrl} class="hidden group-hover:block" alt="Logo" width="168" height="40"/>
                    {:else}
                        <img src={LogoSymbolUrl} class="group-hover:hidden" alt="Logo" width="43" height="40"/>
                        <img src={LogoUrl} class="hidden group-hover:block" alt="Logo" width="168" height="40"/>
                    {/if}
                {/if}
            </a>
        </div>
        {#if $sidebar.expanded}
            <ul 
                data-testid="sidebar-expanded-menuList"
                class="overflow-y-auto p-3 pb-10 min-h-[100vh - 65px] w-[280px] ">
                {#each menuConfig as menu}
                    {#if menu.type === "dropdown-menu"}
                        <li 
                            data-testid="sidebar-expanded-dropdown-menu"
                            class="relative">
                            <button
                                onclick={(e) => { e.stopPropagation(); toggleDropdown(menu.name); }}
                                class={cn("w-full flex items-center justify-between gap-3 px-4 py-2.5 rounded-lg transition-colors",
                                    firstDepth === menu.name ? "bg-primary text-white" : "",
                                    firstDepth !== menu.name && openMenus.has(menu.name) ? "bg-gray-100 dark:bg-gray-700" : "",
                                    firstDepth === menu.name || openMenus.has(menu.name) ? "" : "text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800"
                                )}
                            >
                                <div class="flex items-center gap-3">
                                <Icon name={menu.lucideIcon} size="md"/>
                                <span class="font-medium">{menu.innerText}</span>
                                </div>
                                <div class="flex items-center justify-center transition-transform duration-200 {openMenus.has(menu.name) ? 'rotate-180' : ''}">
                                    <Icon name="ChevronDown" size="md"/>
                                </div>
                            </button>
                            <div 
                                class="overflow-hidden transition-all duration-200 flex {openMenus.has(menu.name) ? 'max-h-96 opacity-100' : 'max-h-0 opacity-0'}"
                            >
                                <div class="min-h-full w-[1px] bg-gray-100 dark:bg-gray-700 mx-6 my-2"></div>
                                <div class="mt-1 space-y-1 w-full">
                                    {#each menu.submenuList as submenu}
                                        <a 
                                            href={submenu.url}
                                            class={cn("block px-4 py-2 rounded-lg text-sm transition-colors",
                                                secondDepth === submenu.name ? "bg-gray-100 dark:bg-gray-800 text-primary dark:text-primary-400 font-medium" : "hover:bg-gray-100 dark:hover:bg-gray-800 text-gray-600 dark:text-gray-400 "
                                            )}
                                        >
                                            {submenu.innerText}
                                        </a>
                                    {/each}
                                </div>
                            </div>
                        </li>
                    {:else if menu.type === "menu"}
                        <li
                            data-testid="sidebar-expanded-menu"    
                        >
                            <a 
                            href={menu.url}
                            class={cn("flex items-center gap-3 px-4 py-2.5 rounded-lg transition-colors",
                                firstDepth === menu.name ? "bg-primary text-white" : "text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800"
                            )}
                            >
                                <Icon name={menu.lucideIcon} size="md"/>
                                <span class="font-medium">{menu.innerText}</span>
                            </a>
                        </li>      
                    {:else if menu.type === "title"}
                        <li
                            data-testid="sidebar-expanded-title"    
                        >
                            <p class="py-1 mt-1 px-2 text-gray-500 dark:text-gray-400 font-semibold">{menu.innerText}</p>
                        </li>
                    {/if}
                {/each}
            </ul>
        {:else}
            <ul class="overflow-y-auto p-3 pb-10 min-h-[100vh - 65px] group-hover:w-[280px]">
            {#each menuConfig as menu}
                {#if menu.type === "dropdown-menu"}
                    <li class={cn("mx-auto w-fit flex justify-center p-2 rounded-lg group-hover:hidden my-1",
                        firstDepth === menu.name ? "bg-primary text-white": "text-gray-400 dark:text-gray-500"
                    )}>
                        <Icon name={menu.lucideIcon} size="lg"/>
                    </li>
                    <li class="relative hidden group-hover:block">
                        <button
                            onclick={(e) => { e.stopPropagation(); toggleDropdown(menu.name); }}
                            class={cn("w-full flex items-center justify-between gap-3 px-4 py-2.5 rounded-lg transition-colors",
                                firstDepth === menu.name ? "bg-primary text-white" : "",
                                firstDepth !== menu.name && openMenus.has(menu.name) ? "bg-gray-100 dark:bg-gray-700" : "",
                                firstDepth === menu.name || openMenus.has(menu.name) ? "" : "text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800"
                            )}
                        >
                            <div class="flex items-center gap-3">
                            <Icon name={menu.lucideIcon} size="md"/>
                            <span class="font-medium">{menu.innerText}</span>
                            </div>
                            <div class="flex items-center justify-center transition-transform duration-200 {openMenus.has(menu.name) ? 'rotate-180' : ''}">
                                <Icon name="ChevronDown" size="md"/>
                            </div>
                        </button>
                        <div 
                            class="overflow-hidden transition-all duration-200 flex {openMenus.has(menu.name) ? 'max-h-96 opacity-100' : 'max-h-0 opacity-0'}"
                        >
                            <div class="min-h-full w-[1px] bg-gray-100 dark:bg-gray-700 mx-6 my-2"></div>
                            <div class="mt-1 space-y-1 w-full">
                                {#each menu.submenuList as submenu}
                                    <a 
                                        href={submenu.url}
                                        class={cn("block px-4 py-2 rounded-lg text-sm transition-colors",
                                            secondDepth === submenu.name ? "bg-gray-100 dark:bg-gray-800 text-primary dark:text-primary-400 font-medium" : "hover:bg-gray-100 dark:hover:bg-gray-800 text-gray-600 dark:text-gray-400 "
                                        )}
                                    >
                                        {submenu.innerText}
                                    </a>
                                {/each}
                            </div>
                        </div>
                    </li>
                {:else if menu.type === "menu"}
                    <li class={cn("mx-auto w-fit flex justify-center p-2 rounded-lg group-hover:hidden my-1",
                            firstDepth === menu.name ? "bg-primary text-white": "text-gray-400 dark:text-gray-500"
                        )}>
                            <Icon name={menu.lucideIcon} size="lg"/>
                    </li>
                    <li class="hidden group-hover:block">
                        <a 
                            href={menu.url}
                            class={cn("flex items-center gap-3 px-4 py-2.5 rounded-lg transition-colors",
                                firstDepth === menu.name ? "bg-primary text-white" : "text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800"
                            )}
                            >
                                <Icon name={menu.lucideIcon} size="md"/>
                                <span class="font-medium">{menu.innerText}</span>
                        </a>
                    </li>      
                {:else if menu.type === "title"}
                    <li>
                        <p class="py-1 mt-1 px-2 text-gray-500 dark:text-gray-400 font-semibold hidden group-hover:block">{menu.innerText}</p>
                    </li>
                {/if}
            {/each}
            </ul>
        {/if}
    </nav>
</div>

