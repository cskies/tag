/**
 * @file 	    SequenceControl.cs
 * @note        Copyright INPE - Instituto Nacional de Pesquisas Espaciais, Grupo de Supervisao de Bordo
 * @brief       Este arquivo faz parte do Software de Monitoramento e Controle Remoto do projeto COMAV.
 * @author 	    Fabricio de Novaes Kucinskis
 * @date	    14/07/2009
 * @note	    Modificado em 02/03/2015 por Conrado: Correcao do BUG SIA_OBC_SW_BUG-32.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * @Namespace Este Namespace possui recursos para controlar o envio e recepcao dos pacotes.
 */ 
namespace Inpe.Subord.Comav.Egse.Smc.Ccsds.Application
{    
    #region Classe SequenceControlUnit

    /**
     * @class SequenceControlUnit
     * Classe que possui as variaveis para controlar a sequencia dos Apids.
     */ 
      class SequenceControlUnit
      {   
          public int apid;
          public int lastSent;
          public int lastReceived;
      }

    #endregion

    #region Classe SequenceControl
     
     /**
      * @class SequenceControl
      * Classe que possui os metodos que controlam a sequencia dos Apids.
      */ 
      public class SequenceControl
      {
          #region Atributos

          private List<SequenceControlUnit> sequenceCounters = new List<SequenceControlUnit>();

          #endregion

          #region Construtor

          public SequenceControl()
          {
          }

          #endregion

          #region Metodos Privados

          private void AddNewApid()
          {
              SequenceControlUnit sequenceControlUnits = new SequenceControlUnit();

              sequenceControlUnits.apid = 255;
              sequenceControlUnits.lastSent = 0;
              sequenceControlUnits.lastReceived = 0;

              sequenceCounters.Add(sequenceControlUnits);
          }

          #endregion

          #region Metodos Publicos

          public void SetLastSent(int apid, int ssc)
          {
              if ((apid == 0) || (apid == 255)) return; // time_packet ou idle_paclet                          
              int index = GetApidIndex(apid);

              sequenceCounters[index].lastSent = ssc;
          }

          public void IncrementSent(int apid)
          {
              if ((apid == 0) || (apid == 255)) return; // time_packet ou idle_paclet                        
              int index = GetApidIndex(apid);

              sequenceCounters[index].lastSent++;
          }

          public void IncrementReceived(int apid)
          {
              if ((apid == 0) || (apid == 255)) return; // time_packet ou idle_paclet
              int index = GetApidIndex(apid);

              sequenceCounters[index].lastReceived++;
          }

          public void RestartSent(int apid)
          {
              if ((apid == 0) || (apid == 255)) return; // time_packet ou idle_paclet
              int index = GetApidIndex(apid);

              sequenceCounters[index].lastSent = 1;
          }

          public void RestartReceived(int apid)
          {
              if ((apid == 0) || (apid == 255)) return; // time_packet ou idle_paclet
              int index = GetApidIndex(apid);

              sequenceCounters[index].lastReceived = 1;
          }

          public int GetLastSent(int apid)
          {
              //06-01-15 Conrado Moura - Correção do BUG SIA_OBC_SW_BUG-32
              //ALTERADO (ANTES RETORNAVA 0 E SEQUENCE_COUNT ZERAVA QUANDO APID = 0000)
              if ((apid == 0) || (apid == 255)) return (0); // time_packet ou idle_paclet
              int index = GetApidIndex(apid);

              return (sequenceCounters[index].lastSent);
          }

          public int GetLastReceived(int apid)
          {
              // Conrado Moura, atualizacao feita em 06-01-15 relacionada ao BUG SIA_OBC_SW_BUG-32.
              // Esta funcao retornada -1 e agora retorna 0 para nao afetar o incremento do campo Sequence Count que por sua vez inicia-se em 1. 
              // O numero de sequencia zero eh reservado pelo padrao PUS.
              if ((apid == 0) || (apid == 255)) return (0); // time_packet ou idle_paclet
              int index = GetApidIndex(apid);

              return (sequenceCounters[index].lastReceived);
          }

          public int GetApidIndex(int apid)
          {
              for (int i = 0; i < sequenceCounters.Count; i++)
              {
                  if (sequenceCounters[i].apid == apid)
                  {
                      return (i);
                  }
                  else if (sequenceCounters[i].apid == 255) // idle_packet
                  {
                      // o apid passado ainda nao se encontra no vetor
                      sequenceCounters[i].apid = apid;
                      return (i);
                  }
              }

              AddNewApid();
              int newIndex = (sequenceCounters.Count - 1);
              sequenceCounters[newIndex].apid = apid;

              // retorna o novo index que foi adicionado na Collection dos APIDs
              return (newIndex);
          }

          #endregion
      }

      #endregion
}