<script>
    /**
     * CMS - 태그 목록 페이지
     * Space별로 태그 관리
     */
    import Icon from "$lib/icons/icon.svelte";
    import { Pagination } from "$lib/components/ui";

    // Space 목록 (필터용)
    let spaces = $state([
        { id: 0, name: '전체' },
        { id: 1, name: 'Tech Blog' },
        { id: 2, name: 'Product Docs' },
        { id: 3, name: 'Internal Wiki' },
    ]);

    let tags = $state([
        { id: '1', name: 'Svelte', description: 'Svelte 관련 콘텐츠', spaceId: 1, spaceName: 'Tech Blog', contentCount: 12, createdAt: '2024-01-10T09:00:00.000Z' },
        { id: '2', name: 'TypeScript', description: 'TypeScript 관련 콘텐츠', spaceId: 1, spaceName: 'Tech Blog', contentCount: 8, createdAt: '2024-01-15T10:30:00.000Z' },
        { id: '3', name: '.NET', description: '.NET 관련 콘텐츠', spaceId: 1, spaceName: 'Tech Blog', contentCount: 15, createdAt: '2024-02-01T14:20:00.000Z' },
        { id: '4', name: 'API Guide', description: 'API 가이드 태그', spaceId: 2, spaceName: 'Product Docs', contentCount: 5, createdAt: '2024-02-10T11:00:00.000Z' },
    ]);

    let selectedSpaceId = $state(0);
    let searchQuery = $state('');
    let currentPage = $state(1);
    const totalItems = 24;
    const itemsPerPage = 10;
    const totalPages = Math.ceil(totalItems / itemsPerPage);

    function formatDate(dateString) {
        return new Date(dateString).toLocaleDateString('ko-KR', {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        });
    }

    function handleSearch(e) {
        e.preventDefault();
        console.log('Search:', { spaceId: selectedSpaceId, query: searchQuery });
    }
</script>

<div class="grid grid-cols-1 gap-4">
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
            <p class="text-lg font-medium text-gray-800 dark:text-gray-100">태그 관리</p>
            <a href="/apps/cms/tags/new" class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors flex items-center">
                <Icon name="Plus" size="sm" class="inline mr-1" />
                새 태그
            </a>
        </header>

        <!-- 필터 & 검색 -->
        <div class="px-6 py-4 border-b border-gray-100 dark:border-gray-800 bg-gray-50 dark:bg-gray-800/50">
            <form onsubmit={handleSearch} class="flex items-center gap-4">
                <!-- Space 필터 -->
                <select
                    bind:value={selectedSpaceId}
                    class="px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                >
                    {#each spaces as space}
                        <option value={space.id}>{space.name}</option>
                    {/each}
                </select>

                <input
                    type="text"
                    bind:value={searchQuery}
                    placeholder="태그명으로 검색"
                    class="flex-1 px-3 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 text-sm focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-colors"
                />
                <button type="submit" class="p-2 text-gray-500 hover:text-primary hover:bg-white dark:hover:bg-gray-800 rounded-lg transition-colors">
                    <Icon name="Search" size="sm" />
                </button>
            </form>
        </div>

        <!-- 테이블 -->
        <section class="p-6">
            <table class="w-full">
                <thead>
                    <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">태그</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Space</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">콘텐츠 수</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">생성일</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-20"></th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                    {#each tags as tag}
                        <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                            <td class="px-4 py-3">
                                <div>
                                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{tag.name}</p>
                                    <p class="text-xs text-gray-500 dark:text-gray-400">{tag.description || '-'}</p>
                                </div>
                            </td>
                            <td class="px-4 py-3">
                                <span class="text-sm text-gray-600 dark:text-gray-400">{tag.spaceName}</span>
                            </td>
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {tag.contentCount}
                            </td>
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {formatDate(tag.createdAt)}
                            </td>
                            <td class="px-4 py-3">
                                <a
                                    href="/apps/cms/tags/{tag.id}/edit"
                                    class="p-1.5 text-gray-400 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors inline-block"
                                >
                                    <Icon name="Pencil" size="sm"/>
                                </a>
                            </td>
                        </tr>
                    {/each}
                </tbody>
            </table>
        </section>

        <footer class="px-6 py-4 border-t border-gray-100 dark:border-gray-800">
            <Pagination bind:currentPage={currentPage} {totalPages} {totalItems} {itemsPerPage} />
        </footer>
    </div>
</div>
