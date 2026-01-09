
<script>
	import { onMount } from 'svelte';
	import { theme } from '$lib/stores/theme.js';
	import '../app.css';
	import favicon from '$lib/assets/favicon.svg';
	onMount(() => {
		theme.init();
	});
</script>

<svelte:head>
	<link rel="icon" href={favicon} />
</svelte:head>


<slot />