<script>
	import { onMount } from 'svelte';
	import { theme } from '$lib/stores/theme.js';
	import '../app.css';
	let { children } = $props();
	import favicon from '$lib/assets/favicon.svg';
  import Navbar from '$lib/components/layouts/navbar.svelte';
  import Sidebar from '$lib/components/layouts/sidebar.svelte';
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
	<main class="mt-[64px] ml-[280px] p-6">
		{@render children()}
	</main>
</div>