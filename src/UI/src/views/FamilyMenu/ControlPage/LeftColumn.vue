<template>
  <div class="col-left">
    <Title text="The name of the organization" style="transform: translate(0, -50%);" />
    <Input
      style="margin-top: 0.741vh;margin-bottom: 4.907vh;"
      v-model="currentOrgName"
      :placeholder="loc('familyMenu_91')"
      :showButtons="organizationName.value !== currentOrgName"
      @onSave="saveOrganizationName"
      @onCancel="refreshData"
    />
    <Title text="Removal of the organization" />
    <div class="about">
Sind Sie sicher, dass Sie die Organisation entfernen möchten? Wenn Sie die Organisation löschen,
Dann verschwinden alle Immobilien der Organisation.
    </div>
    <DefaultBtn @click="setShowModal">delete</DefaultBtn>
    <Title text="Display family members on the map" />
    <div class="row">
      <Switcher :value="mapMember" @input="saveMapOptions" />
      <div>Mitglieder der Familie in der Nähe anzeigen</div>
    </div>
    <Title text="Transport management" />
    <div class="vehicle">
      <DefaultBtn @click="setShowModalVeh" class="secondary"
        >Gehen Sie zu den Einstellungen</DefaultBtn
      >
      <img src="/img/familyMenu/veh-settings.svg" alt="" />
    </div>
    <ModalDelete v-if="showModal" @closeModal="setShowModal" />
    <VehicleTab v-if="showModalVeh" @closeModal="setShowModalVeh" />
  </div>
</template>

<script>
import Title from './components/Title.vue'
import Switcher from './components/Switcher.vue'
import DefaultBtn from '../../UI/button/DefaultBtn.vue'
import { mapState, mapGetters } from 'vuex'
import ModalDelete from './modals/ModalDelete.vue'
import Input from './components/Input.vue'
import VehicleTab from './modals/ModalVehicles.vue'

export default {
  components: { Title, DefaultBtn, Switcher, ModalDelete, Input, VehicleTab },
  data: function() {
    return {
      currentOrgName: null,
      isEdited: null,
      showModal: false,
      showModalVeh: false,
      mapMember: null,
    }
  },

  computed: {
    ...mapState('familyMenu', ['isLeader', 'membersOnMap']),
    ...mapState('familyMenu/controlPage', ['organizationName']),
    ...mapGetters('localization', ['loc']),
  },

  methods: {
    refreshData: function() {
      this.currentOrgName = this.organizationName.value
      this.isEdited = false
    },
    saveOrganizationName: function() {
      window.mp.trigger('familyMenu:saveOrganizationName', this.currentOrgName)
      this.isEdited = true
    },
    setShowModal: function() {
      this.showModal = !this.showModal
    },
    setShowModalVeh: function() {
      this.showModalVeh = !this.showModalVeh
    },
    saveMapOptions: function() {
      this.mapMember = !this.mapMember
      window.mp.trigger('familyMenu:saveMapOptions', this.mapMember)
    },
    loadOptions: function() {
      this.mapMember = this.membersOnMap
    },
  },

  mounted() {
    this.refreshData()
    this.loadOptions()
  },
}
</script>

<style lang="scss" scoped>
div,
span,
button,
input {
  font-family: 'Akrobat';
  font-style: normal;
  color: #ffffff;
}

.col-left {
  width: 41.944vh;
  height: 100%;
  position: relative;

  .about {
    margin-top: 1.296vh;
    width: 40.463vh;
    font-weight: 700;
    font-size: 1.852vh;
    line-height: 2.222vh;
    text-transform: uppercase;
    color: rgba(255, 255, 255, 0.44);
  }
  button {
    margin-top: 2.13vh;
    margin-bottom: 2.685vh;
    width: 27.315vh;
    height: 6.944vh;
    text-transform: uppercase;
    font-weight: 700;
    font-size: 2.222vh;
    line-height: 2.685vh;
    &.secondary {
      background: rgba(255, 255, 255, 0.05);
      &:hover {
        background: rgba(255, 255, 255, 0.12);
      }
    }
  }
  .row {
    margin: 5.185vh 0 5vh 3.519vh;
    display: flex;
    gap: 0.833vh;
    font-weight: 700;
    font-size: 1.852vh;
    line-height: 2.222vh;
    text-transform: uppercase;
  }
  .vehicle {
    display: flex;
    justify-content: space-between;
    align-items: center;
    img {
      margin-right: 5.093vh;
      width: 3.981vh;
      height: 3.981vh;
    }
  }
  &::after {
    content: '';
    position: absolute;
    top: 0;
    right: 0;
    width: 0.185vh;
    height: 100%;
    background: linear-gradient(
      180deg,
      rgba(255, 255, 255, 0.2) 70%,
      rgba(255, 255, 255, 0) 100%
    );
  }
}
</style>
