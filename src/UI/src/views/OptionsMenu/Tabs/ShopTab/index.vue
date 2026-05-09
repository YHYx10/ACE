<template>
    <div class="shop-page">
        <template v-if="!isExclusive && !isObject && !isAnimal && !isCurrencyGame">
            <div class="shop-page-left">
            <PremiumCard :premium="shop.premiumPrice" class="card-hover"/>
            </div>
            <div class="shop-page-centre">
                <ExclusiveCard v-if="shop.exclusive.count < shop.exclusive.maxcount" @click.native="openExclusive" :exclusive="shop.exclusive" class="card-hover"/>
                <ExclusiveCard v-else style="opacity: 0.1;" :exclusive="shop.exclusive" class="card-hover"/>
                <div class="shop-page-centre-bottom">
                    <AnimalsCard @click.native="openAnimal" class="card-hover" />
                    <!-- @click.native="openAnimal" -->
                    <!-- @click.native="openObject -->
                    <ObjectCard @click.native="openObject" class="card-hover"/>
                </div>
            </div>
            <div class="shop-page-right">
                <CurrencyGameCard class="card-hover" @click.native="openCurrencyGame"/>>
            </div>
        </template>
        
        <transition-group tag="div" name="modal">
            <ExclusiveModal @close="isExclusive = false" :exclusive="shop.exclusive" :key="1" v-show="isExclusive" />
            <ObjectModal :key="2" v-show="isObject" />
            <AnimalModal @close="isAnimal = false" :key="3" v-show="isAnimal"/>
            <CurrencyGameModal @close="isCurrencyGame = false" :money="shop.money" :key="4" v-show="isCurrencyGame"/>
            <!--<transport-modal :key="1" v-if="isTransportModal" @close="isTransportModal = false" />
            <business-modal :key="2" v-if="isBusinessModal" @close="isBusinessModal = false" />-->
        </transition-group>
    </div>
</template>

<script>
import { mapState } from 'vuex'
import PremiumCard from './common/PremiumCard.vue';
import ExclusiveCard from './common/ExclusiveCard.vue';
import AnimalsCard from './common/AnimalsCard.vue';
import ObjectCard from './common/ObjectCard.vue';
import CurrencyGameCard from './common/CurrencyGameCard.vue';
import ExclusiveModal from './ExclusiveModal/index.vue';
import ObjectModal from './ObjectModal/index.vue'
import AnimalModal from "@/views/OptionsMenu/Tabs/ShopTab/AnimalModal";
import CurrencyGameModal from "@/views/OptionsMenu/Tabs/ShopTab/CurrencyGameModal";

export default {
    name: "ShopTab",
    data() {
        return {
            isExclusive: false,
            isObject: false,
            isAnimal: false,
            isCurrencyGame: false,
            sizeModifier: 62
        }
    },
    computed: {
        ...mapState("optionsMenu", ["shop"]),
        appWidthUnit: function () {
            const appWidth = document.getElementById('app').offsetWidth;
            const appHeight = document.getElementById('app').offsetHeight;
            let widthUnit = null;
            if (appWidth/appHeight > 1.7) {
                widthUnit = Math.ceil(appHeight/this.sizeModifier) + 'px';
            } else {
                widthUnit = Math.ceil(appWidth/100) + 'px';
            }
            return widthUnit;
        },
    },
    methods: {
        openExclusive() {
            this.isExclusive = true
        },
        openObject() {
            this.isObject = true
        },
        openAnimal() {
        this.isAnimal = true
        },
        openCurrencyGame() {
          this.isCurrencyGame = true
        },
    },
    mounted() {
        console.log('SHOPP: '+this.shop)
        this.sizeModifier = 54
        const htmlTag = document.getElementsByTagName('html')[0];
        htmlTag.style.fontSize = this.appWidthUnit;
        // console.log('FONTSIZE 1:', this.appWidthUnit)
    },
    beforeDestroy(){
        this.sizeModifier = 54
        const htmlTag = document.getElementsByTagName('html')[0];
        htmlTag.style.fontSize = this.appWidthUnit;
        // console.log('FONTSIZE 2:', this.appWidthUnit)
    },
    components: {
      CurrencyGameModal,
      AnimalModal, PremiumCard, ExclusiveCard, AnimalsCard, ObjectCard, CurrencyGameCard, ExclusiveModal, ObjectModal }
}
</script>

<style lang="scss" scoped>
.shop-page {
    display: flex;
    margin-top: 2rem;
    max-width: 86rem;
}

.shop-page-left {
    margin-right: 1.2rem;
}

.shop-page-centre {
    margin-right: 1.2rem;

    &-bottom {
        display: flex;
        margin-top: 1.2rem;
    }
}


.card-hover {
  //transition: background-color 0.4s ease;
  &:hover {
    //transform: scale(1.04);
    // background-color: #1E1E1E;

  }
}
</style>