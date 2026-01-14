<script>
	import { onMount } from 'svelte';
	import { sidebar, theme } from "$lib/stores";
	import '../app.css';
	let { children } = $props();
	import favicon from '$lib/assets/favicon.svg';
	import Navbar from '$lib/components/layouts/navbar.svelte';
	import Sidebar from '$lib/components/layouts/sidebar.svelte';
  import { cn } from '$lib/utils';
	onMount(() => {
		theme.init();
	});
</script>

<svelte:head>
	<link rel="icon" href={favicon} />
</svelte:head>

<div class="min-h-screen flex flex-col m-0 bg-gray-50 dark:bg-gray-950">
    <Navbar/>
    <Sidebar/>
	<main class={cn("mt-[64px] p-6", $sidebar.expanded ? "ml-[280px]" : "ml-[84px]")}>
		{@render children()}
	</main>
</div>