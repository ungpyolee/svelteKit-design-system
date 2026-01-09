<script>
    import { sidebar, theme } from '$lib/stores';
    import LogoUrl from '$lib/assets/logo.png';
    import LogoLightUrl from '$lib/assets/logo-light.png';
    import LogoSymbolUrl from '$lib/assets/logo-icon.png';
    import LogoSymbolLightUrl from '$lib/assets/logo-icon-light.png';
</script>
<a href="/" title="Logo 로고" class="py-3 px-5 w-full">
    {#if $sidebar.expanded}
        {#if $theme === 'dark'}
        <img src={LogoLightUrl} alt="Logo" width="168" height="40"/>
        {:else}
            <img src={LogoUrl} alt="Logo" width="168" height="40"/>
        {/if}
    {:else}
        {#if $theme === 'dark'}
            <img src={LogoSymbolLightUrl} alt="Logo" width="43" height="40"/>
        {:else}
            <img src={LogoSymbolUrl} alt="Logo" width="43" height="40"/>
        {/if}
    {/if}
    
</a>