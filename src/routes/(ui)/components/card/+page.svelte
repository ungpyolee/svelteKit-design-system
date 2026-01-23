<script>
  import Icon from "$lib/icons/icon.svelte";

</script>
<div class="grid grid-colos-1 gap-8">
    <div>
        <p class="text-2xl mb-2 ps-2 text-gray-800 dark:text-gray-100">Card Header & Footer</p>
        <div class="grid grid-cols-1 lg:grid-cols-2 2xl:grid-cols-3 gap-4">
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="px-6 pt-6">
                    <p class="text-lg font-medium">Title</p>
                </header>
                <section class="p-6">
                    <p class="text-gray-700 dark:text-gray-200">Content text</p>
                </section>
            </div>
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="p-6 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">Title</p>
                </header>
                <section class="p-6">
                    <p class="text-gray-700 dark:text-gray-200 line-clamp-5">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>
                </section>
            </div>
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl overflow-hidden">
                <header class="p-6 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100 ">Title</p>
                </header>
                <section class="p-6">
                    <p class="text-gray-700 dark:text-gray-200 line-clamp-5">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>
                </section>
                <footer class="p-6 border-t border-gray-100 dark:border-gray-800 flex">
                    <button class="px-6 py-2 bg-primary hover:bg-primary-600 rounded-sm hover:bg-primary-light ms-auto text-white transition-colors">
                        Button
                    </button>
                </footer>
            </div>
        </div>
    </div>
    <div>
        <p class="text-2xl mb-2 ps-2 text-gray-800 dark:text-gray-100">Card Text & icon</p>
        <div class="grid grid-cols-1 lg:grid-cols-2 2xl:grid-cols-4 gap-4">
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl h-fit">
                <section class="p-6 flex">
                    <div class="w-full flex flex-col gap-2">
                        <p class="text-md text-gray-600 dark:text-gray-200">Card title</p>
                        <p class="text-3xl">0</p>
                    </div>    
                    <div class="min-w-14 min-h-14 flex items-center">
                        <div class="w-14 h-14 rounded-full bg-primary dark:bg-primary-700 text-white flex items-center justify-center">
                            <Icon name="Layers" size="lg"/>
                        </div>
                    </div>
                </section>
                <footer class="px-6 pb-6">
                    <a href="#" class="hover:underline text-primary">Read more</a>
                </footer>
            </div>
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl h-fit">
                <section class="p-6 flex">
                    <div class="w-full flex flex-col gap-2">
                        <p class="text-md text-gray-600 dark:text-gray-200">Card title</p>
                        <p class="text-3xl">0</p>
                    </div>    
                    <div class="min-w-14 min-h-14 flex items-center">
                        <div class="w-14 h-14 rounded-full bg-primary text-white flex items-center justify-center">
                            <Icon name="Layers" size="lg"/>
                        </div>
                    </div>
                </section>
                <footer class="px-6 pb-6">
                    <p class="text-gray-500 line-clamp-1">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>
                </footer>
            </div>
        </div>
    </div>
</div>