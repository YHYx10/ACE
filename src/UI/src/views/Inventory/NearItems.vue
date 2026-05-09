<template>
    <div class="near">
        <ListItems 
            :id="0"
            type="near"
            :drag="drag"
            :width="2"             
            :sortByIndex ="false"
            :canDrop="false"
            @onMouseDown="onMouseDown"  
            @onDoubleClick="onDoubleClick"
        />        
    </div>
</template>

<script>
import { mapGetters } from 'vuex';
import ListItems from './ListItems';


export default {
    props: ['drag'],
    methods: {
        onMouseDown(dragObject){
            this.$emit("onMouseDown", dragObject);
        },
        onDoubleClick(adress, item){
            this.$emit("onDoubleClick", adress, item);
        }
    },
    components:{
        ListItems
    },
    computed: {
        items(){
            const items = [...this.inventory.items];
            return items;
        },
        ...mapGetters('localization', ['loc'])
    },
}
</script>

<style lang="scss" scoped>
    .near{
        width: 30vh;
        height: 100%;
    }
</style>