<script>
    import Icon from "$lib/icons/icon.svelte";


    // 파일 업로드
    let uploadedFiles = $state([]);
    let thumbnailFile = $state(null);
    let thumbnailPreview = $state('');


       // 파일 업로드
    function handleFileUpload(e) {
        const files = Array.from(e.target.files);
        const newFiles = files.map(file => ({
            id: crypto.randomUUID(),
            file: file,
            displayName: file.name,
            description: '',
            fileSize: file.size,
            fileExtension: '.' + file.name.split('.').pop(),
        }));
        uploadedFiles = [...uploadedFiles, ...newFiles];
    }

    function removeFile(fileId) {
        uploadedFiles = uploadedFiles.filter(f => f.id !== fileId);
    }

    function updateFileDescription(fileId, description) {
        uploadedFiles = uploadedFiles.map(f => 
            f.id === fileId ? { ...f, description } : f
        );
    }

    // 썸네일 업로드
    function handleThumbnailUpload(e) {
        const file = e.target.files[0];
        if (file) {
            thumbnailFile = file;
            const reader = new FileReader();
            reader.onload = (e) => {
                thumbnailPreview = e.target.result;
            };
            reader.readAsDataURL(file);
        }
    }

    function removeThumbnail() {
        thumbnailFile = null;
        thumbnailPreview = '';
    }

    // 파일 크기 포맷
    function formatFileSize(bytes) {
        if (bytes < 1024) return bytes + ' B';
        if (bytes < 1048576) return (bytes / 1024).toFixed(1) + ' KB';
        return (bytes / 1048576).toFixed(1) + ' MB';
    }

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
            '.sch': 'FileCode',
        };
        return icons[extension] || 'File';
    }
</script>
<div class="grid grid-cols-2 gap-6">
    <!-- 썸네일 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <p class="text-lg font-medium text-gray-800 dark:text-gray-100">썸네일</p>
        </header>
        <section class="p-6">
            {#if thumbnailPreview}
                <div class="relative">
                    <img 
                        src={thumbnailPreview} 
                        alt="Thumbnail preview"
                        class="w-full aspect-video object-cover rounded-lg"
                    />
                    <button 
                        type="button"
                        onclick={removeThumbnail}
                        class="absolute top-2 right-2 p-1.5 bg-black/50 text-white rounded-lg hover:bg-black/70 transition-colors"
                    >
                        <Icon name="X" size="sm" />
                    </button>
                </div>
            {:else}
                <label class="block">
                    <div class="border-2 border-dashed border-gray-200 dark:border-gray-700 rounded-lg p-6 text-center hover:border-primary dark:hover:border-primary transition-colors cursor-pointer aspect-video flex flex-col items-center justify-center">
                        <Icon name="Image" size="lg" class="text-gray-400 mb-2" />
                        <p class="text-sm text-gray-500 dark:text-gray-400">썸네일 이미지 업로드</p>
                    </div>
                    <input 
                        type="file" 
                        accept="image/*"
                        onchange={handleThumbnailUpload}
                        class="hidden"
                    />
                </label>
            {/if}
        </section>
    </div>

    <!-- 첨부파일 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
        <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
            <p class="text-lg font-medium text-gray-800 dark:text-gray-100">첨부파일</p>
        </header>
        <section class="p-6 space-y-4">
            <!-- 파일 업로드 영역 -->
            <label class="block">
                <div class="border-2 border-dashed border-gray-200 dark:border-gray-700 rounded-lg p-8 text-center hover:border-primary dark:hover:border-primary transition-colors cursor-pointer">
                    <div class="w-12 h-12 mx-auto mb-4 rounded-full bg-gray-100 dark:bg-gray-800 flex items-center justify-center text-gray-400">
                        <Icon name="Upload" size="lg" />
                    </div>
                    <p class="text-gray-600 dark:text-gray-400 mb-1">파일을 업로드</p>
                    <p class="text-sm text-gray-400 dark:text-gray-500">PDF, DWG, DOC, XLS 등 (최대 100MB)</p>
                </div>
                <input 
                    type="file" 
                    multiple
                    onchange={handleFileUpload}
                    class="hidden"
                />
            </label>

            <!-- 업로드된 파일 목록 -->
            {#if uploadedFiles.length > 0}
                <div class="space-y-2">
                    {#each uploadedFiles as file}
                        <div class="flex items-start gap-3 p-3 bg-gray-50 dark:bg-gray-800 rounded-lg">
                            <div class="w-10 h-10 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400 flex-shrink-0">
                                <Icon name={getFileIcon(file.fileExtension)} size="md" />
                            </div>
                            <div class="flex-1 min-w-0">
                                <p class="text-sm font-medium text-gray-800 dark:text-gray-200 truncate">{file.displayName}</p>
                                <p class="text-xs text-gray-500 dark:text-gray-400">{formatFileSize(file.fileSize)}</p>
                                <input 
                                    type="text"
                                    placeholder="파일 설명 (선택)"
                                    value={file.description}
                                    oninput={(e) => updateFileDescription(file.id, e.target.value)}
                                    class="mt-2 w-full px-3 py-1.5 text-sm border border-gray-200 dark:border-gray-700 rounded bg-white dark:bg-gray-900 text-gray-800 dark:text-gray-200 focus:ring-1 focus:ring-primary outline-none"
                                />
                            </div>
                            <button 
                                type="button"
                                onclick={() => removeFile(file.id)}
                                class="p-1.5 text-gray-400 hover:text-danger hover:bg-gray-200 dark:hover:bg-gray-700 rounded transition-colors"
                            >
                                <Icon name="Trash" size="sm" />
                            </button>
                        </div>
                    {/each}
                </div>
            {/if}
        </section>
    </div>
</div>