<!-- /rms/digital-assets/[id]/+page.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import Modal from "$lib/components/ui/Modal.svelte";
    import Pagination from "$lib/components/ui/Pagination.svelte";
    import { cn } from "$lib/utils";
    
    // 더미 데이터 (실제로는 API에서 가져옴)
    let asset = $state({
        id: "a1b2c3d4-5678-9012-abcd-ef1234567890",
        assetNumber: "IPM-2024-0892",
        title: "고효율 영구자석 동기전동기 설계 도면",
        subTitle: "IPM 모터 냉각 구조 및 자석 배치 상세 설계",
        bodyCopy: "본 설계 도면은 고효율 IPMSM 모터의 전체 구조를 포함합니다. 냉각 시스템 설계, V자형 자석 배치, 로터/스테이터 상세 치수가 포함되어 있으며, 전기차 구동 모터 적용을 목표로 설계되었습니다. 최대 효율 97.2%, 정격 출력 150kW 사양입니다.",
        tags: ["IPMSM", "전기차", "냉각설계", "고효율", "150kW"],
        filters: {
            MachineType: {
                id: "mt-001",
                displayName: "IPMSM",
                description: "Interior Permanent Magnet Synchronous Motor"
            },
            AssetType: {
                id: "at-001",
                displayName: "설계도면",
                description: "CAD 설계 도면 및 3D 모델"
            }
        },
        tenantId: "tenant-clew-001",
        isGrantRequiredItem: true,
        disclosureRange: "team",
        listPrice: 50000,
        publishStatus: 1,
        contributor: {
            userId: "user-001",
            name: "김철수",
            email: "cs.kim@clew.tech"
        },
        assetFiles: [
            {
                id: "file-001",
                displayName: "IPM_Motor_Assembly_v1.2.dwg",
                description: "전체 조립도",
                storageKey: "assets/ipm-2024-0892/assembly.dwg",
                contentType: "application/dwg",
                fileExtension: ".dwg",
                fileSizeInByte: 25690112,
                uploadedAt: "2024-01-10T09:30:00.000Z"
            },
            {
                id: "file-002",
                displayName: "IPM_Cooling_System.pdf",
                description: "냉각 시스템 상세 설명서",
                storageKey: "assets/ipm-2024-0892/cooling.pdf",
                contentType: "application/pdf",
                fileExtension: ".pdf",
                fileSizeInByte: 8540160,
                uploadedAt: "2024-01-10T09:35:00.000Z"
            }
        ],
        metaTag: {
            title: "IPM-2024-0892 고효율 IPMSM 설계 도면",
            keywords: "IPMSM, 전기차 모터, 냉각설계, 영구자석",
            description: "150kW급 고효율 IPMSM 모터의 상세 설계 도면입니다.",
            thumbnailKey: "https://rms-api.motorspace.org/images/thumbnails/088c1cbb-670a-4767-9f02-69209dfc85bc/af55379d-cc8c-46a5-b864-4689b07329f7.png"
        },
        createdAt: "2024-01-10T09:00:00.000Z",
        updatedAt: "2024-01-15T14:20:00.000Z"
    });

    // 해당 자료의 요청 목록
    let assetRequests = $state([
        {
            requestId: 1,
            requestDate: '2024-01-15',
            requester: '정다운',
            requesterEmail: 'dw.jung@motorspace.io',
            requesterTeam: 'Motorspace',
            requestDescription: '신규 전기차 프로젝트에 적용할 IPM 모터 설계 검토를 위해 해당 도면이 필요합니다.',
            status: 'approved',
            approver: '박영희',
            processedDate: '2024-01-16',
            processDescription: '프로젝트 목적 확인 완료. 외부 유출 금지 조건 부여.',
        },
        {
            requestId: 2,
            requestDate: '2024-01-18',
            requester: '김태호',
            requesterEmail: 'th.kim@geneers.com',
            requesterTeam: 'GENEERS',
            requestDescription: '모터 효율 분석을 위해 설계 도면 검토가 필요합니다.',
            status: 'pending',
            approver: null,
            processedDate: null,
            processDescription: null,
        },
        {
            requestId: 3,
            requestDate: '2024-01-20',
            requester: '이수진',
            requesterEmail: 'sj.lee@clew.tech',
            requesterTeam: 'CLEW',
            requestDescription: '내부 교육 자료 제작을 위해 설계 도면이 필요합니다.',
            status: 'rejected',
            approver: '김대표',
            processedDate: '2024-01-21',
            processDescription: '교육 자료용으로는 별도 요약본을 제공 예정입니다.',
        },
    ]);

    // 상태 설정
    const statusConfig = {
        approved: { label: '승인', class: 'bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400' },
        pending: { label: '대기중', class: 'bg-warning-100 text-warning-700 dark:bg-warning-900/30 dark:text-warning-400' },
        rejected: { label: '반려', class: 'bg-danger-100 text-danger-700 dark:bg-danger-900/30 dark:text-danger-400' }
    };

    const disclosureConfig = {
        team: { label: '팀 내 공개', class: 'bg-info-100 text-info-700 dark:bg-info-900/30 dark:text-info-400' },
        organization: { label: '조직 내 공개', class: 'bg-primary-100 text-primary-700 dark:bg-primary-900/30 dark:text-primary-400' },
        restricted: { label: '제한 공개', class: 'bg-danger-100 text-danger-700 dark:bg-danger-900/30 dark:text-danger-400' }
    };

    // 파일 아이콘
    function getFileIcon(extension) {
        const icons = {
            '.pdf': 'FileText',
            '.dwg': 'FileBox',
            '.doc': 'FileText',
            '.docx': 'FileText',
            '.xls': 'FileSpreadsheet',
            '.xlsx': 'FileSpreadsheet',
            '.zip': 'FileArchive',
        };
        return icons[extension] || 'File';
    }

    // 파일 크기 포맷
    function formatFileSize(bytes) {
        if (bytes < 1024) return bytes + ' B';
        if (bytes < 1048576) return (bytes / 1024).toFixed(1) + ' KB';
        return (bytes / 1048576).toFixed(1) + ' MB';
    }

    // 날짜 포맷
    function formatDate(dateString) {
        return new Date(dateString).toLocaleDateString('ko-KR', {
            year: 'numeric',
            month: '2-digit',
            day: '2-digit'
        });
    }

    // 가격 포맷
    function formatPrice(price) {
        return new Intl.NumberFormat('ko-KR').format(price) + '원';
    }

    // 요청 상세 모달
    let showRequestModal = $state(false);
    let selectedRequest = $state(null);

    function openRequestDetail(request) {
        selectedRequest = request;
        showRequestModal = true;
    }

    // 승인/반려 모달
    let showProcessModal = $state(false);
    let processType = $state('');
    let processDescription = $state('');

    function openProcessModal(request, type) {
        selectedRequest = request;
        processType = type;
        processDescription = '';
        showProcessModal = true;
    }

    function handleProcess() {
        console.log({
            requestId: selectedRequest.requestId,
            type: processType,
            description: processDescription
        });
        showProcessModal = false;
    }

    // 필터
    let statusFilter = $state('all');
    let filteredRequests = $derived(
        statusFilter === 'all' 
            ? assetRequests 
            : assetRequests.filter(r => r.status === statusFilter)
    );

    // 페이지네이션
    let currentPage = $state(1);
    const itemsPerPage = 10;
</script>

<div class="space-y-6">
    <!-- 브레드크럼 -->
    <nav class="flex items-center gap-2 text-sm text-gray-500 dark:text-gray-400">
        <a href="/rms/digital-assets" class="hover:text-primary">Digital Assets</a>
        <Icon name="ChevronRight" size="xs" />
        <span class="text-gray-800 dark:text-gray-200">{asset.assetNumber}</span>
    </nav>

    <!-- 헤더 섹션 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl overflow-hidden">
        <div class="relative">
            <!-- 썸네일 이미지 -->
            <div class="h-48 bg-gradient-to-r from-primary-500 to-primary-700 relative">
                <img 
                    src={asset.metaTag.thumbnailKey} 
                    alt={asset.title}
                    class="w-full h-full object-cover opacity-30"
                />
                <div class="absolute inset-0 flex items-end p-6">
                    <div class="flex-1">
                        <div class="flex items-center gap-2 mb-2">
                            <span class="px-2 py-1 text-xs font-medium bg-white/20 text-white rounded">
                                {asset.assetNumber}
                            </span>
                            <span class="px-2 py-1 text-xs font-medium bg-white/20 text-white rounded">
                                {asset.filters.AssetType.displayName}
                            </span>
                            <span class="px-2 py-1 text-xs font-medium bg-white/20 text-white rounded">
                                {asset.filters.MachineType.displayName}
                            </span>
                        </div>
                        <h1 class="text-2xl font-bold text-white mb-1">{asset.title}</h1>
                        <p class="text-white/80">{asset.subTitle}</p>
                    </div>
                </div>
            </div>
        </div>

        <!-- 액션 버튼 -->
        <div class="px-6 py-4 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
            <div class="flex items-center gap-4">
                <span class={cn("px-2 py-1 text-xs font-medium rounded-full", disclosureConfig[asset.disclosureRange]?.class)}>
                    {disclosureConfig[asset.disclosureRange]?.label}
                </span>
                {#if asset.isGrantRequiredItem}
                    <span class="flex items-center gap-1 text-sm text-warning-600 dark:text-warning-400">
                        <Icon name="ShieldAlert" size="sm" />
                        승인 필요
                    </span>
                {/if}
                <span class="text-lg font-semibold text-gray-800 dark:text-gray-200">
                    {formatPrice(asset.listPrice)}
                </span>
            </div>
            <div class="flex items-center gap-2">
                <a 
                    href="/rms/digital-assets/{asset.id}/edit"
                    class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 border border-gray-200 dark:border-gray-700 rounded-lg transition-colors"
                >
                    <Icon name="Pencil" size="sm" class="inline mr-1" />
                    수정
                </a>
                <button class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors">
                    <Icon name="Download" size="sm" class="inline mr-1" />
                    다운로드
                </button>
            </div>
        </div>

        <!-- 상세 정보 -->
        <div class="p-6">
            <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
                <!-- 왼쪽: 설명 -->
                <div class="lg:col-span-2 space-y-6">
                    <!-- 설명 -->
                    <div>
                        <h3 class="text-sm font-semibold text-gray-800 dark:text-gray-200 mb-2">설명</h3>
                        <p class="text-gray-600 dark:text-gray-400 leading-relaxed">
                            {asset.bodyCopy}
                        </p>
                    </div>

                    <!-- 태그 -->
                    <div>
                        <h3 class="text-sm font-semibold text-gray-800 dark:text-gray-200 mb-2">태그</h3>
                        <div class="flex flex-wrap gap-2">
                            {#each asset.tags as tag}
                                <span class="px-3 py-1 text-sm bg-gray-100 dark:bg-gray-800 text-gray-700 dark:text-gray-300 rounded-full">
                                    #{tag}
                                </span>
                            {/each}
                        </div>
                    </div>

                    <!-- 첨부파일 -->
                    <div>
                        <h3 class="text-sm font-semibold text-gray-800 dark:text-gray-200 mb-3">
                            첨부파일 ({asset.assetFiles.length})
                        </h3>
                        <div class="space-y-2">
                            {#each asset.assetFiles as file}
                                <div class="flex items-center justify-between p-3 bg-gray-50 dark:bg-gray-800 rounded-lg hover:bg-gray-100 dark:hover:bg-gray-700 transition-colors">
                                    <div class="flex items-center gap-3">
                                        <div class="w-10 h-10 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400">
                                            <Icon name={getFileIcon(file.fileExtension)} size="md" />
                                        </div>
                                        <div>
                                            <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{file.displayName}</p>
                                            <p class="text-xs text-gray-500 dark:text-gray-400">
                                                {file.description} · {formatFileSize(file.fileSizeInByte)}
                                            </p>
                                        </div>
                                    </div>
                                    <button class="p-2 text-gray-400 hover:text-primary hover:bg-gray-200 dark:hover:bg-gray-600 rounded-lg transition-colors">
                                        <Icon name="Download" size="sm" />
                                    </button>
                                </div>
                            {/each}
                        </div>
                    </div>
                </div>

                <!-- 오른쪽: 메타 정보 -->
                <div class="space-y-4">
                    <!-- 등록자 정보 -->
                    <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                        <h3 class="text-xs text-gray-500 dark:text-gray-400 mb-3">등록자</h3>
                        <div class="flex items-center gap-3">
                            <div class="w-10 h-10 rounded-full bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400 font-semibold">
                                {asset.contributor.name.charAt(0)}
                            </div>
                            <div>
                                <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{asset.contributor.name}</p>
                                <p class="text-xs text-gray-500 dark:text-gray-400">{asset.contributor.email}</p>
                            </div>
                        </div>
                    </div>

                    <!-- 기본 정보 -->
                    <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg space-y-3">
                        <h3 class="text-xs text-gray-500 dark:text-gray-400">기본 정보</h3>
                        <div class="space-y-2">
                            <div class="flex justify-between">
                                <span class="text-sm text-gray-500 dark:text-gray-400">자료 번호</span>
                                <span class="text-sm font-medium text-gray-800 dark:text-gray-200">{asset.assetNumber}</span>
                            </div>
                            <div class="flex justify-between">
                                <span class="text-sm text-gray-500 dark:text-gray-400">전동기 타입</span>
                                <span class="text-sm font-medium text-gray-800 dark:text-gray-200">{asset.filters.MachineType.displayName}</span>
                            </div>
                            <div class="flex justify-between">
                                <span class="text-sm text-gray-500 dark:text-gray-400">자료 타입</span>
                                <span class="text-sm font-medium text-gray-800 dark:text-gray-200">{asset.filters.AssetType.displayName}</span>
                            </div>
                            <div class="flex justify-between">
                                <span class="text-sm text-gray-500 dark:text-gray-400">등록일</span>
                                <span class="text-sm font-medium text-gray-800 dark:text-gray-200">{formatDate(asset.createdAt)}</span>
                            </div>
                            <div class="flex justify-between">
                                <span class="text-sm text-gray-500 dark:text-gray-400">최종 수정일</span>
                                <span class="text-sm font-medium text-gray-800 dark:text-gray-200">{formatDate(asset.updatedAt)}</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- 요청 목록 섹션 -->
    <div id="requests" class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800 flex items-center justify-between">
            <div class="flex items-center gap-3">
                <p class="text-lg font-medium text-gray-800 dark:text-gray-100">
                    요청 목록
                </p>
                <span class="px-2 py-0.5 text-xs font-medium bg-gray-100 dark:bg-gray-800 text-gray-600 dark:text-gray-400 rounded-full">
                    {assetRequests.length}건
                </span>
            </div>
            <div class="flex items-center gap-2">
                <select 
                    bind:value={statusFilter}
                    class="px-3 py-1.5 text-sm border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-700 dark:text-gray-300 focus:ring-2 focus:ring-primary focus:border-transparent outline-none"
                >
                    <option value="all">전체 상태</option>
                    <option value="pending">대기중</option>
                    <option value="approved">승인</option>
                    <option value="rejected">반려</option>
                </select>
            </div>
        </header>
        <section class="p-6">
            {#if filteredRequests.length > 0}
                <table class="w-full">
                    <thead>
                        <tr class="bg-gray-50 dark:bg-gray-800 text-left">
                            <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">요청자</th>
                            <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">요청 사유</th>
                            <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">요청일</th>
                            <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">상태</th>
                            <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300">처리자</th>
                            <th class="px-4 py-3 text-sm font-semibold text-gray-700 dark:text-gray-300 w-32"></th>
                        </tr>
                    </thead>
                    <tbody class="divide-y divide-gray-100 dark:divide-gray-800">
                        {#each filteredRequests as request}
                            <tr class="hover:bg-gray-50 dark:hover:bg-gray-800/50 transition-colors">
                                <td class="px-4 py-3">
                                    <div class="flex items-center gap-3">
                                        <div class="w-8 h-8 rounded-full bg-gray-200 dark:bg-gray-700 flex items-center justify-center text-gray-600 dark:text-gray-300 text-sm font-medium">
                                            {request.requester.charAt(0)}
                                        </div>
                                        <div>
                                            <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{request.requester}</p>
                                            <p class="text-xs text-gray-500 dark:text-gray-400">{request.requesterTeam}</p>
                                        </div>
                                    </div>
                                </td>
                                <td class="px-4 py-3">
                                    <p class="text-sm text-gray-600 dark:text-gray-400 truncate max-w-[200px]">
                                        {request.requestDescription}
                                    </p>
                                </td>
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                    {request.requestDate}
                                </td>
                                <td class="px-4 py-3">
                                    <span class="px-2 py-1 text-xs font-medium {statusConfig[request.status].class} rounded-full">
                                        {statusConfig[request.status].label}
                                    </span>
                                </td>
                                <td class="px-4 py-3 text-sm text-gray-600 dark:text-gray-400">
                                    {request.approver || '-'}
                                </td>
                                <td class="px-4 py-3">
                                    {#if request.status === 'pending'}
                                        <div class="flex items-center gap-1">
                                            <button 
                                                onclick={() => openProcessModal(request, 'approve')}
                                                class="px-2 py-1 text-xs font-medium bg-success-100 text-success-700 hover:bg-success-200 dark:bg-success-900/30 dark:text-success-400 dark:hover:bg-success-900/50 rounded transition-colors"
                                            >
                                                승인
                                            </button>
                                            <button 
                                                onclick={() => openProcessModal(request, 'reject')}
                                                class="px-2 py-1 text-xs font-medium bg-danger-100 text-danger-700 hover:bg-danger-200 dark:bg-danger-900/30 dark:text-danger-400 dark:hover:bg-danger-900/50 rounded transition-colors"
                                            >
                                                반려
                                            </button>
                                        </div>
                                    {:else}
                                        <button 
                                            onclick={() => openRequestDetail(request)}
                                            class="p-1.5 text-gray-400 hover:text-primary hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                                        >
                                            <Icon name="ChevronRight" size="sm" />
                                        </button>
                                    {/if}
                                </td>
                            </tr>
                        {/each}
                    </tbody>
                </table>
            {:else}
                <div class="text-center py-12">
                    <div class="w-16 h-16 mx-auto mb-4 rounded-full bg-gray-100 dark:bg-gray-800 flex items-center justify-center text-gray-400">
                        <Icon name="Inbox" size="lg" />
                    </div>
                    <p class="text-gray-500 dark:text-gray-400">요청 내역이 없습니다.</p>
                </div>
            {/if}
        </section>
        {#if filteredRequests.length > itemsPerPage}
            <footer class="px-6 py-4 border-t border-gray-100 dark:border-gray-800">
                <Pagination 
                    bind:currentPage={currentPage}
                    totalPages={Math.ceil(filteredRequests.length / itemsPerPage)}
                    totalItems={filteredRequests.length}
                    itemsPerPage={itemsPerPage}
                />
            </footer>
        {/if}
    </div>
</div>

<!-- 요청 상세 모달 -->
<Modal bind:isOpen={showRequestModal} title="요청 상세" size="lg">
    {#if selectedRequest}
        <div class="space-y-6">
            <!-- 요청자 정보 -->
            <div class="p-4 border border-gray-100 dark:border-gray-800 rounded-lg">
                <p class="text-xs text-gray-500 dark:text-gray-400 mb-3">요청자 정보</p>
                <div class="flex items-center gap-3">
                    <div class="w-10 h-10 rounded-full bg-gray-200 dark:bg-gray-700 flex items-center justify-center text-gray-600 dark:text-gray-300 font-medium">
                        {selectedRequest.requester.charAt(0)}
                    </div>
                    <div>
                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedRequest.requester}</p>
                        <p class="text-xs text-gray-500 dark:text-gray-400">{selectedRequest.requesterEmail} · {selectedRequest.requesterTeam}</p>
                    </div>
                </div>
            </div>

            <!-- 요청 사유 -->
            <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                <p class="text-xs text-gray-500 dark:text-gray-400 mb-2">요청 사유</p>
                <p class="text-sm text-gray-700 dark:text-gray-300">{selectedRequest.requestDescription}</p>
                <p class="text-xs text-gray-400 mt-2">요청일: {selectedRequest.requestDate}</p>
            </div>

            <!-- 처리 정보 -->
            {#if selectedRequest.status !== 'pending'}
                <div class="p-4 border-l-4 {selectedRequest.status === 'approved' ? 'border-success bg-success-50 dark:bg-success-900/10' : 'border-danger bg-danger-50 dark:bg-danger-900/10'} rounded-r-lg">
                    <div class="flex items-center justify-between mb-2">
                        <p class="text-xs font-medium {selectedRequest.status === 'approved' ? 'text-success-700 dark:text-success-400' : 'text-danger-700 dark:text-danger-400'}">
                            {selectedRequest.status === 'approved' ? '승인' : '반려'} 사유
                        </p>
                        <p class="text-xs text-gray-500 dark:text-gray-400">{selectedRequest.processedDate}</p>
                    </div>
                    <p class="text-sm text-gray-700 dark:text-gray-300 mb-3">{selectedRequest.processDescription}</p>
                    <div class="flex items-center gap-2 text-xs text-gray-500 dark:text-gray-400">
                        <span>처리자:</span>
                        <span class="font-medium text-gray-700 dark:text-gray-300">{selectedRequest.approver}</span>
                    </div>
                </div>
            {/if}
        </div>
    {/if}

    {#snippet footer()}
        <button 
            onclick={() => showRequestModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            닫기
        </button>
    {/snippet}
</Modal>

<!-- 승인/반려 처리 모달 -->
<Modal bind:isOpen={showProcessModal} title={processType === 'approve' ? '요청 승인' : '요청 반려'} size="md">
    {#if selectedRequest}
        <div class="space-y-4">
            <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                <div class="flex items-center gap-3 mb-3">
                    <div class="w-8 h-8 rounded-full bg-gray-200 dark:bg-gray-700 flex items-center justify-center text-gray-600 dark:text-gray-300 text-sm font-medium">
                        {selectedRequest.requester.charAt(0)}
                    </div>
                    <div>
                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200">{selectedRequest.requester}</p>
                        <p class="text-xs text-gray-500 dark:text-gray-400">{selectedRequest.requesterTeam}</p>
                    </div>
                </div>
                <p class="text-sm text-gray-600 dark:text-gray-400">{selectedRequest.requestDescription}</p>
            </div>
            
            <div>
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                    {processType === 'approve' ? '승인' : '반려'} 사유 <span class="text-danger">*</span>
                </label>
                <textarea 
                    bind:value={processDescription}
                    rows="4"
                    placeholder={processType === 'approve' ? '승인 사유를 입력하세요...' : '반려 사유를 입력하세요...'}
                    class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all resize-none"
                ></textarea>
            </div>
        </div>
    {/if}

    {#snippet footer()}
        <button 
            onclick={() => showProcessModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            취소
        </button>
        <button 
            onclick={handleProcess}
            disabled={!processDescription.trim()}
            class={cn(
                "px-4 py-2 text-sm font-medium text-white rounded-lg transition-colors disabled:opacity-50 disabled:cursor-not-allowed",
                processType === 'approve' 
                    ? "bg-success hover:bg-success-600" 
                    : "bg-danger hover:bg-danger-600"
            )}
        >
            {processType === 'approve' ? '승인' : '반려'}
        </button>
    {/snippet}
</Modal>