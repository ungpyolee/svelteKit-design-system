<script>
  import Icon from "$lib/icons/icon.svelte";

</script>
<div class="grid grid-cols-1 gap-8">
    <div>
        <p class="text-2xl mb-2 ps-2 text-gray-800 dark:text-gray-100">Basic Tables</p>
        <div class="grid grid-cols-1 2xl:grid-cols-2 gap-4">
            <!-- Default Table Small-->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="p-6 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">Default Table Small</p>
                </header>
                <section class="p-6">
                    <table class="w-full">
                        <thead>
                            <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Name</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Date</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Status</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Action</th>
                            </tr>
                        </thead>
                        <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                            <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                                <td class="px-4 py-3 text-sm text-gray-800 dark:text-gray-200">John Doe</td>
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">2024-01-15</td>
                                <td class="px-4 py-3">
                                    <span class="px-2 py-1 text-xs font-medium bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400 rounded-full">Active</span>
                                </td>
                                <td class="px-4 py-3">
                                    <button class="text-primary hover:text-primary-700 text-sm font-medium">Edit</button>
                                </td>
                            </tr>
                            <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                                <td class="px-4 py-3 text-sm text-gray-800 dark:text-gray-200">Jane Smith</td>
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">2024-01-12</td>
                                <td class="px-4 py-3">
                                    <span class="px-2 py-1 text-xs font-medium bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400 rounded-full">Pending</span>
                                </td>
                                <td class="px-4 py-3">
                                    <button class="text-primary hover:text-primary-700 text-sm font-medium">Edit</button>
                                </td>
                            </tr>
                            <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                                <td class="px-4 py-3 text-sm text-gray-800 dark:text-gray-200">Mike Johnson</td>
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">2024-01-10</td>
                                <td class="px-4 py-3">
                                    <span class="px-2 py-1 text-xs font-medium bg-danger-100 text-danger-700 dark:bg-danger-900/30 dark:text-danger-400 rounded-full">Inactive</span>
                                </td>
                                <td class="px-4 py-3">
                                    <button class="text-primary hover:text-primary-700 text-sm font-medium">Edit</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </section>
            </div>

            <!-- Default Table -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="p-6 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">Default Table</p>
                </header>
                <section class="p-6">
                    <table class="w-full">
                        <thead>
                            <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                                <th class="px-6 py-3 font-semibold text-gray-700 dark:text-gray-300">Name</th>
                                <th class="px-6 py-3 font-semibold text-gray-700 dark:text-gray-300">Date</th>
                                <th class="px-6 py-3 font-semibold text-gray-700 dark:text-gray-300">Status</th>
                                <th class="px-6 py-3 font-semibold text-gray-700 dark:text-gray-300">Action</th>
                            </tr>
                        </thead>
                        <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                            <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                                <td class="px-6 py-3 text-gray-800 dark:text-gray-200">John Doe</td>
                                <td class="px-6 py-3 text-gray-600 dark:text-gray-400">2024-01-15</td>
                                <td class="px-6 py-3">
                                    <span class="px-2 py-1 text-xs font-medium bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400 rounded-full">Active</span>
                                </td>
                                <td class="px-6 py-3">
                                    <button class="text-primary hover:text-primary-700 font-medium">Edit</button>
                                </td>
                            </tr>
                            <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                                <td class="px-6 py-3 text-gray-800 dark:text-gray-200">Jane Smith</td>
                                <td class="px-6 py-3 text-gray-600 dark:text-gray-400">2024-01-12</td>
                                <td class="px-6 py-3">
                                    <span class="px-2 py-1 text-xs font-medium bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400 rounded-full">Pending</span>
                                </td>
                                <td class="px-6 py-3">
                                    <button class="text-primary hover:text-primary-700 font-medium">Edit</button>
                                </td>
                            </tr>
                            <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                                <td class="px-6 py-3 text-gray-800 dark:text-gray-200">Mike Johnson</td>
                                <td class="px-6 py-3 text-gray-600 dark:text-gray-400">2024-01-10</td>
                                <td class="px-6 py-3">
                                    <span class="px-2 py-1 text-xs font-medium bg-danger-100 text-danger-700 dark:bg-danger-900/30 dark:text-danger-400 rounded-full">Inactive</span>
                                </td>
                                <td class="px-6 py-3">
                                    <button class="text-primary hover:text-primary-700 font-medium">Edit</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </section>
            </div>

            <!-- Default Table Large -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="p-6 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">Default Table Large</p>
                </header>
                <section class="p-6">
                    <table class="w-full">
                        <thead>
                            <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                                <th class="py-4 px-6 text-lg font-semibold text-gray-700 dark:text-gray-300">Name</th>
                                <th class="py-4 px-6 text-lg font-semibold text-gray-700 dark:text-gray-300">Date</th>
                                <th class="py-4 px-6 text-lg font-semibold text-gray-700 dark:text-gray-300">Status</th>
                                <th class="py-4 px-6 text-lg font-semibold text-gray-700 dark:text-gray-300">Action</th>
                            </tr>
                        </thead>
                        <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                            <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                                <td class="py-4 px-6 text-lg text-gray-800 dark:text-gray-200">John Doe</td>
                                <td class="py-4 px-6 text-lg text-gray-600 dark:text-gray-400">2024-01-15</td>
                                <td class="py-4 px-6">
                                    <span class="px-2 py-1 text-xs font-medium bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400 rounded-full">Active</span>
                                </td>
                                <td class="py-4 px-6">
                                    <button class="text-primary hover:text-primary-700 text-lg font-medium">Edit</button>
                                </td>
                            </tr>
                            <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                                <td class="py-4 px-6 text-lg text-gray-800 dark:text-gray-200">Jane Smith</td>
                                <td class="py-4 px-6 text-lg text-gray-600 dark:text-gray-400">2024-01-12</td>
                                <td class="py-4 px-6">
                                    <span class="px-2 py-1 text-xs font-medium bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400 rounded-full">Pending</span>
                                </td>
                                <td class="py-4 px-6">
                                    <button class="text-primary hover:text-primary-700 text-lg font-medium">Edit</button>
                                </td>
                            </tr>
                            <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                                <td class="py-4 px-6 text-lg text-gray-800 dark:text-gray-200">Mike Johnson</td>
                                <td class="py-4 px-6 text-lg text-gray-600 dark:text-gray-400">2024-01-10</td>
                                <td class="py-4 px-6">
                                    <span class="px-2 py-1 text-xs font-medium bg-danger-100 text-danger-700 dark:bg-danger-900/30 dark:text-danger-400 rounded-full">Inactive</span>
                                </td>
                                <td class="py-4 px-6">
                                    <button class="text-primary hover:text-primary-700 text-lg font-medium">Edit</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </section>
            </div>

            <!-- Striped Table -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="p-6 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">Striped Table</p>
                </header>
                <section class="p-6">
                    <table class="w-full">
                        <thead>
                            <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Name</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Date</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Status</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr class="bg-white dark:bg-gray-900">
                                <td class="px-4 py-3 text-sm text-gray-800 dark:text-gray-200">John Doe</td>
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">2024-01-15</td>
                                <td class="px-4 py-3">
                                    <span class="px-2 py-1 text-xs font-medium bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400 rounded-full">Active</span>
                                </td>
                                <td class="px-4 py-3">
                                    <button class="text-primary hover:text-primary-700 text-sm font-medium">Edit</button>
                                </td>
                            </tr>
                            <tr class="bg-gray-50 dark:bg-gray-800/50">
                                <td class="px-4 py-3 text-sm text-gray-800 dark:text-gray-200">Jane Smith</td>
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">2024-01-12</td>
                                <td class="px-4 py-3">
                                    <span class="px-2 py-1 text-xs font-medium bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400 rounded-full">Pending</span>
                                </td>
                                <td class="px-4 py-3">
                                    <button class="text-primary hover:text-primary-700 text-sm font-medium">Edit</button>
                                </td>
                            </tr>
                            <tr class="bg-white dark:bg-gray-900">
                                <td class="px-4 py-3 text-sm text-gray-800 dark:text-gray-200">Mike Johnson</td>
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">2024-01-10</td>
                                <td class="px-4 py-3">
                                    <span class="px-2 py-1 text-xs font-medium bg-danger-100 text-danger-700 dark:bg-danger-900/30 dark:text-danger-400 rounded-full">Inactive</span>
                                </td>
                                <td class="px-4 py-3">
                                    <button class="text-primary hover:text-primary-700 text-sm font-medium">Edit</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </section>
            </div>

            <!-- Bordered Table -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="p-6 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">Bordered Table</p>
                </header>
                <section class="p-6">
                    <table class="w-full border border-gray-200 dark:border-gray-700">
                        <thead>
                            <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 border border-gray-200 dark:border-gray-700">Name</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 border border-gray-200 dark:border-gray-700">Date</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 border border-gray-200 dark:border-gray-700">Status</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 border border-gray-200 dark:border-gray-700">Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="px-4 py-3 text-sm text-gray-800 dark:text-gray-200 border border-gray-200 dark:border-gray-700">John Doe</td>
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400 border border-gray-200 dark:border-gray-700">2024-01-15</td>
                                <td class="px-4 py-3 border border-gray-200 dark:border-gray-700">
                                    <span class="px-2 py-1 text-xs font-medium bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400 rounded-full">Active</span>
                                </td>
                                <td class="px-4 py-3 border border-gray-200 dark:border-gray-700">
                                    <button class="text-primary hover:text-primary-700 text-sm font-medium">Edit</button>
                                </td>
                            </tr>
                            <tr>
                                <td class="px-4 py-3 text-sm text-gray-800 dark:text-gray-200 border border-gray-200 dark:border-gray-700">Jane Smith</td>
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400 border border-gray-200 dark:border-gray-700">2024-01-12</td>
                                <td class="px-4 py-3 border border-gray-200 dark:border-gray-700">
                                    <span class="px-2 py-1 text-xs font-medium bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400 rounded-full">Pending</span>
                                </td>
                                <td class="px-4 py-3 border border-gray-200 dark:border-gray-700">
                                    <button class="text-primary hover:text-primary-700 text-sm font-medium">Edit</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </section>
            </div>

            <!-- Compact Table -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="p-6 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">Compact Table</p>
                </header>
                <section class="p-6">
                    <table class="w-full">
                        <thead>
                            <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                                <th class="px-3 py-2 text-xs font-semibold text-gray-700 dark:text-gray-300">Name</th>
                                <th class="px-3 py-2 text-xs font-semibold text-gray-700 dark:text-gray-300">Date</th>
                                <th class="px-3 py-2 text-xs font-semibold text-gray-700 dark:text-gray-300">Status</th>
                                <th class="px-3 py-2 text-xs font-semibold text-gray-700 dark:text-gray-300">Action</th>
                            </tr>
                        </thead>
                        <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                            <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                                <td class="px-3 py-2 text-xs text-gray-800 dark:text-gray-200">John Doe</td>
                                <td class="px-3 py-2 text-xs text-gray-600 dark:text-gray-400">2024-01-15</td>
                                <td class="px-3 py-2">
                                    <span class="px-1.5 py-0.5 text-xs font-medium bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400 rounded">Active</span>
                                </td>
                                <td class="px-3 py-2">
                                    <button class="text-primary hover:text-primary-700 text-xs font-medium">Edit</button>
                                </td>
                            </tr>
                            <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                                <td class="px-3 py-2 text-xs text-gray-800 dark:text-gray-200">Jane Smith</td>
                                <td class="px-3 py-2 text-xs text-gray-600 dark:text-gray-400">2024-01-12</td>
                                <td class="px-3 py-2">
                                    <span class="px-1.5 py-0.5 text-xs font-medium bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400 rounded">Pending</span>
                                </td>
                                <td class="px-3 py-2">
                                    <button class="text-primary hover:text-primary-700 text-xs font-medium">Edit</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </section>
            </div>


            <!-- Checkbox Table -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="p-6 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">Checkbox Table</p>
                </header>
                <section class="p-6">
                    <table class="w-full">
                        <thead>
                            <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                                <th class="px-4 py-3 w-12">
                                    <input 
                                        type="checkbox" 
                                        class="w-4 h-4 rounded border-gray-300 dark:border-gray-600 text-primary focus:ring-primary dark:bg-gray-700"
                                    />
                                </th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Name</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Email</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Role</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Status</th>
                                <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Action</th>
                            </tr>
                        </thead>
                        <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                            <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                                <td class="px-4 py-3">
                                    <input 
                                        type="checkbox" 
                                        class="w-4 h-4 rounded border-gray-300 dark:border-gray-600 text-primary focus:ring-primary dark:bg-gray-700"
                                    />
                                </td>
                                <td class="px-4 py-3">
                                    <div class="flex items-center gap-3">
                                        <div class="w-8 h-8 rounded-full bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400 text-sm font-medium">JD</div>
                                        <span class="text-sm text-gray-800 dark:text-gray-200">John Doe</span>
                                    </div>
                                </td>
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">john@example.com</td>
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">Admin</td>
                                <td class="px-4 py-3">
                                    <span class="px-2 py-1 text-xs font-medium bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400 rounded-full">Active</span>
                                </td>
                                <td class="px-4 py-3">
                                    <div class="flex items-center gap-2">
                                        <button class="p-1.5 text-gray-500 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded transition-colors">
                                            <Icon name="Pencil" size="sm"/>
                                        </button>
                                        <button class="p-1.5 text-gray-500 hover:text-danger hover:bg-gray-100 dark:hover:bg-gray-800 rounded transition-colors">
                                            <Icon name="Trash" size="sm"/>
                                        </button>
                                    </div>
                                </td>
                            </tr>
                            <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                                <td class="px-4 py-3">
                                    <input 
                                        type="checkbox" 
                                        class="w-4 h-4 rounded border-gray-300 dark:border-gray-600 text-primary focus:ring-primary dark:bg-gray-700"
                                    />
                                </td>
                                <td class="px-4 py-3">
                                    <div class="flex items-center gap-3">
                                        <div class="w-8 h-8 rounded-full bg-warning-100 dark:bg-warning-900/30 flex items-center justify-center text-warning-600 dark:text-warning-400 text-sm font-medium">JS</div>
                                        <span class="text-sm text-gray-800 dark:text-gray-200">Jane Smith</span>
                                    </div>
                                </td>
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">jane@example.com</td>
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">Editor</td>
                                <td class="px-4 py-3">
                                    <span class="px-2 py-1 text-xs font-medium bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400 rounded-full">Pending</span>
                                </td>
                                <td class="px-4 py-3">
                                    <div class="flex items-center gap-2">
                                        <button class="p-1.5 text-gray-500 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded transition-colors">
                                            <Icon name="Pencil" size="sm"/>
                                        </button>
                                        <button class="p-1.5 text-gray-500 hover:text-danger hover:bg-gray-100 dark:hover:bg-gray-800 rounded transition-colors">
                                            <Icon name="Trash" size="sm"/>
                                        </button>
                                    </div>
                                </td>
                            </tr>
                            <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                                <td class="px-4 py-3">
                                    <input 
                                        type="checkbox" 
                                        class="w-4 h-4 rounded border-gray-300 dark:border-gray-600 text-primary focus:ring-primary dark:bg-gray-700"
                                    />
                                </td>
                                <td class="px-4 py-3">
                                    <div class="flex items-center gap-3">
                                        <div class="w-8 h-8 rounded-full bg-danger-100 dark:bg-danger-900/30 flex items-center justify-center text-danger-600 dark:text-danger-400 text-sm font-medium">MJ</div>
                                        <span class="text-sm text-gray-800 dark:text-gray-200">Mike Johnson</span>
                                    </div>
                                </td>
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">mike@example.com</td>
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">Viewer</td>
                                <td class="px-4 py-3">
                                    <span class="px-2 py-1 text-xs font-medium bg-danger-100 text-danger-700 dark:bg-danger-900/30 dark:text-danger-400 rounded-full">Inactive</span>
                                </td>
                                <td class="px-4 py-3">
                                    <div class="flex items-center gap-2">
                                        <button class="p-1.5 text-gray-500 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded transition-colors">
                                            <Icon name="Pencil" size="sm"/>
                                        </button>
                                        <button class="p-1.5 text-gray-500 hover:text-danger hover:bg-gray-100 dark:hover:bg-gray-800 rounded transition-colors">
                                            <Icon name="Trash" size="sm"/>
                                        </button>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </section>
            </div>
        </div>
    </div>
</div>