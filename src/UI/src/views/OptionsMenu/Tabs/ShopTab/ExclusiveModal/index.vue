<template>
    <div class="modal">
        <button class="field__close" @click="$emit('close')">
          <div>
            <img src="/img/inputMenu/arrow.svg" alt="" />
          </div>
          Back
        </button>
        <div class="top-part-exclusive">
            <div class="exclusive-list" v-for="item in exclusive.pictures" :key="item.id">
                <ExclusiveCardModal :pictures="item" :ref="item.title" @modalClickCard="addBorder" class="exclusive-modal-item"/>
            </div>
        </div>

        <!-- <div class="range">
            <div class="range-full"></div>
        </div> -->
    </div>
</template>

<script>
import ExclusiveCardModal from './commons/ExclusiveCardModal.vue';


export default {
    name: "ExclusiveModal",
    props: {
        exclusive: Object
    },
    data() {
        return {
            activeEl: null,
        }

    },
    components: { ExclusiveCardModal },
    mounted() {
        console.log('Com')
        console.log(this.$refs.second[0].$el)
        /*this.$refs.second[0].$el.classList.add('active')*/
    },
    methods: {
        addBorder(id) {
            if (this.activeEl == null) {
                console.log('asd')
                document.getElementById(id).classList.add('active')
                this.activeEl = id
            } else {
                document.getElementById(this.activeEl).classList.remove('active')
                this.activeEl = id
                document.getElementById(this.activeEl).classList.add('active')
            }
        }
    }

}
</script>

<style lang="scss" scoped>
.modal {
    position: absolute;
    /*display: flex;*/
    top: -0.09rem;
    left: 0;
    width: 100%;
    height: 100%;
    z-index: 100;
    // background: url("/img/optionsMenu/bg.png"), rgba(0, 0, 0, 0.96);
    background-blend-mode: overlay;

}

.field {
    &__close {
      display: flex;
      align-items: center;
      background: none;
      outline: none;
      border: none;
      font-weight: 700;
      font-size: 1.053rem;
      line-height: 1.263rem;
      color: #ffffff;
      cursor: pointer;
      margin-bottom: 1.579rem;

      &:hover {
        div {
          background: rgba(255, 255, 255, 0.15);
        }
      }

      div {
        width: 2.474rem;
        height: 2.474rem;
        display: flex;
        align-items: center;
        justify-content: center;
        margin-right: 0.737rem;
        background: rgba(255, 255, 255, 0.07);

        img {
          height: 0.737rem;
          width: auto;
        }
      }
    }
}

.active {
    // border: 0.0495rem solid #5CFF80 !important
}

.top-part-exclusive {
    display: flex;
    margin-bottom: 1.2492rem;
    justify-content: space-between;
}

.exclusive-list {
    display: flex;
}
.exclusive-modal-item {
  //transition: border 0.4s ease;

//   &:hover {
//     //transform: scale(1.04);
//     border: 0.0495rem solid #5CFF80;

//   }
}
.range {
    width: 100%;
    height: 0.099rem;
    overflow: hidden;
    position: relative;
    background: linear-gradient(180deg, rgba(255, 255, 255, 0.25) 0%, rgba(255, 255, 255, 0) 100%);

    &-full {
        position: absolute;
        width: 14.1498rem;
        height: 0.099rem;


        background: #5CFF80;
    }
}
</style>