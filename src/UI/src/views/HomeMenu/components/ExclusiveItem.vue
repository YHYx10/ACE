<template>
    <div class="exclusive-item">
        <p>{{el.id}}</p>
        <p>{{el.title}}</p>
    </div>
</template>

<script>
export default {
    props: {
        el: Object
    }
}
</script>

<style lang="scss">
.exclusive-item {
    width: 549px;
    height: 808px;
    border-radius: 27px;
    background-color: rgb(0, 0, 0);
}
</style>