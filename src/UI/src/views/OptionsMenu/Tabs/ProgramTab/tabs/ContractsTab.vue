<template>
  <div class="contracts-tab">
    <div class="contracts-tab__board">
      <div class="contracts-tab__header">
        <div class="contracts-tab__heading">
          <div class="category">section</div>
          <div class="title">available to me</div>
        </div>
        <div class="contracts-tab__filter">
          <div class="category">Type of contracts</div>
          <div class="contracts-tab__filter-items">
          <div 
            :class="[{active: currentFilter === item.filter}, 'contracts-tab__filter-item']"
            v-for="item in filtersList"
            :key="item.filter"
            @click="setCurrentFilter(item.filter)"
          >
            <div class="title">{{loc(item.desc)}}</div>
          </div>
          </div>
        </div>
      </div>
      <div class="contracts-tab__list">
        <ContractItem
          v-for="(item, index) in currentList"
          :key="index"
          :item="contracts[item]"
          :currentContract="currentContract"
          @setCurrentContract="setCurrentContract"
        />
      </div>
    </div>
    <transition name="current-contract">
      <CurrentContract 
        v-if="currentContract"
        :contract="currentContract"

      />
    </transition>
  </div>
</template>

<script>
import ContractItem from './components/ContractItem'
import CurrentContract from './components/CurrentContract'
import contractConfig from '../../../../../configs/personalEvents/contractConfig'
import { mapGetters, mapState } from 'vuex'
export default {
  name: 'ContractsTab',
  components: {
    ContractItem,
    CurrentContract
  },
  data: function() {
    return {
      currentContract: null,
      currentFilter: null,
      filtersList: [
        {
          desc: 'options_program_5',
          filter: 'Single'
        },
        {
          desc: 'options_program_6',
          filter: 'Family'
        },
        {
          desc: 'options_program_7',
          filter: 'current'
        },
      ]
    }
  },
  computed: {
    ...mapState('optionsMenu', ['myContracts', 'familyContracts']),
    ...mapState('familyMenu', ['inFamily']),
    ...mapGetters('localization', ['loc']),
    currentList: function() {
      let keys = Object.keys(contractConfig).filter(item => contractConfig[item].ContractType == 'Family' && this.inFamily || contractConfig[item].ContractType != 'Family' );
      if (this.currentFilter === 'current') {
        return keys.filter(item => this.myContracts.find(c => c.ContractName == item) && this.myContracts.find(c => c.ContractName == item).InProgress || this.familyContracts.find(c => c.ContractName == item) && this.familyContracts.find(c => c.ContractName == item).InProgress)
      }
      else if (this.currentFilter) {
        return keys.filter(item => contractConfig[item].ContractType == this.currentFilter)
      }
      return keys
    },
    contracts() {
      return contractConfig
    }
  },
  methods: {
    setCurrentFilter: function(value) {
      if (this.currentFilter !== value) {        
        this.currentContract = null
        this.currentFilter = value
      }
      else {
        this.currentFilter = null
      }
    },
    setCurrentContract: function(obj) {
      if (this.currentContract != obj)
        this.currentContract = obj
      else 
        this.currentContract = null
    }
  }
}
</script>

<style lang="scss" scoped>
.contracts-tab{
  flex: 1 1 100%;
  display: flex;
  &__board{
    min-width: 45rem;
    max-width: 45rem;
    margin-right: 1rem;
  }
  &__header{
    display: flex;
    margin: 0 0 1rem 0.7rem;
  }
  &__filter{
    position: relative;
    margin-left: 3rem;
    
    &-items {
      display: flex;
    }
    &-item{
      &:not(&:last-child) {
        margin-right: 1rem;
      }
      .title {
        font-weight: 500;
        font-size: 1.3rem;
        line-height: 1.3;
        color: rgba(255,255,255,0.5)
      }
      &.active .title{
        color: #fff;
      }
    }
  }
  &__list{
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    grid-gap: 0.6rem;
    width: 100%;
    height: 37rem;
    padding-right: 0.6rem;
    overflow-y: auto;

    scrollbar-width: thin;
    scrollbar-color: #5cff80 #444444;

    &::-webkit-scrollbar {
      display: block;
      width: 0.1rem;
      height: 0;
    }

    &::-webkit-scrollbar-track {
      background: #444444;
    }

    &::-webkit-scrollbar-thumb {
      background-color: #5cff80;
    }
  }
}
.current-contract-enter-active {
  opacity: 1;
}
.current-contract-leave-active {
  opacity: 0;
}
.current-contract-enter-active, .current-contract-leave-active {
  transition: .3s;
}
.current-contract-enter, .current-contract-leave-to /* .current-contract-leave-active below version 2.1.8 */ {
  transform: translateX(100%);
}
</style>
