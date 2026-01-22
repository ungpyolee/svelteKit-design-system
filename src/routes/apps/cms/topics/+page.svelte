<script>
    /**
     * CMS - 토픽 목록 페이지
     * Space별로 토픽 관리
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

    let topics = $state([
        { id: '1', name: '프론트엔드', description: '프론트엔드 개발 관련', spaceId: 1, spaceName: 'Tech Blog', contentCount: 25, createdAt: '2024-01-05T09:00:00.000Z' },
        { id: '2', name: '백엔드', description: '백엔드 개발 관련', spaceId: 1, spaceName: 'Tech Blog', contentCount: 18, createdAt: '2024-01-05T09:00:00.000Z' },
        { id: '3', name: 'DevOps', description: '인프라 및 DevOps 관련', spaceId: 1, spaceName: 'Tech Blog', contentCount: 10, createdAt: '2024-01-10T10:30:00.000Z' },
        { id: '4', name: 'API Reference', description: 'API 레퍼런스 문서', spaceId: 2, spaceName: 'Product Docs', contentCount: 7, createdAt: '2024-02-01T14:20:00.000Z' },
    ]);

    let selectedSpaceId = $state(0);
    let searchQuery = $state('');
    let currentPage = $state(1);
    const totalItems = 12;
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
            <p class="text-lg font-medium text-gray-800 dark:text-gray-100">토픽 관리</p>
            <a href="/apps/cms/topics/new" class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors flex items-center">
                <Icon name="Plus" size="sm" class="inline mr-1" />
                새 토픽
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
                    placeholder="토픽명으로 검색"
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
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">토픽</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">Space</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">콘텐츠 수</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">생성일</th>
                        <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-20"></th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                    {#each topics as topic}
                        <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                            <td class="px-4 py-3">
                                <div>
                                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{topic.name}</p>
                                    <p class="text-xs text-gray-500 dark:text-gray-400">{topic.description || '-'}</p>
                                </div>
                            </td>
                            <td class="px-4 py-3">
                                <span class="text-sm text-gray-600 dark:text-gray-400">{topic.spaceName}</span>
                            </td>
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {topic.contentCount}
                            </td>
                            <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                {formatDate(topic.createdAt)}
                            </td>
                            <td class="px-4 py-3">
                                <a
                                    href="/apps/cms/topics/{topic.id}/edit"
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
